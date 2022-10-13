using TemplateDomain.Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using NServiceBus;
using System;
using DStack.Aggregates.EventStoreDB;
using EventStore.Client;

namespace TemplateDomain.App
{
    class EndpointConfigurationFactory
    {
        public EndpointConfiguration Create(IConfiguration config)
        {
            var endpointConfiguration = new EndpointConfiguration(config["NSBus:EndpointName"]);
            endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
            endpointConfiguration.LicensePath("config/license.xml");
            RegisterComponents(config, endpointConfiguration);
            InitializeTransport(config, endpointConfiguration);
            InitializePostgreSQLPersistence(config, endpointConfiguration);
            SetupConventions(endpointConfiguration);
            SetupHeartBeatAndMetrics(config, endpointConfiguration);
            SetupAuditing(config, endpointConfiguration);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }

        void RegisterComponents(IConfiguration config, EndpointConfiguration endpointConfiguration)
            => endpointConfiguration.RegisterComponents(reg =>
            {
                var esAggRep = CreateEventStoreAggregateRepository(config);
                reg.ConfigureComponent(() => esAggRep, DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent(() => config, DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent<TimeProvider>(DependencyLifecycle.InstancePerCall);
                RegisterAggregateInteractors(reg);
            });

            ESAggregateRepository CreateEventStoreAggregateRepository(IConfiguration config)
            {
                var settings = EventStoreClientSettings.Create(config["EventStoreDB:ConnectionString"]);
                var client = new EventStoreClient(settings);
                AssertEventStoreAvailable(client);
                return new ESAggregateRepository(client);

            }

                void AssertEventStoreAvailable(EventStoreClient client)
                    => _ = client.GetStreamMetadataAsync("$ce-Any").Result;

            static void RegisterAggregateInteractors(NServiceBus.ObjectBuilder.IConfigureComponents reg)
            {
                foreach (var type in AggregateInteractorsExtractor.GetInteractors())
                    reg.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);
            }

            static void InitializeTransport(IConfiguration config, EndpointConfiguration endpointConfiguration)
            {
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString(config["RabbitMQ:ConnectionString"]);
            }

            static void InitializePostgreSQLPersistence(IConfiguration config, EndpointConfiguration endpointConfiguration)
            {
                var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
                persistence.TablePrefix("TemplateDomain");

                var dialect = persistence.SqlDialect<SqlDialect.PostgreSql>();
                dialect.JsonBParameterModifier(
                    modifier: parameter =>
                    {
                        var npgsqlParameter = (NpgsqlParameter)parameter;
                        npgsqlParameter.NpgsqlDbType = NpgsqlDbType.Jsonb;
                    });
                persistence.ConnectionBuilder(
                    connectionBuilder: () => new NpgsqlConnection(config["PostgreSQL:ConnectionString"])
                    );
                var subscriptions = persistence.SubscriptionSettings();
                subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            }

            static void SetupConventions(EndpointConfiguration endpointConfiguration)
            {
                var conventions = endpointConfiguration.Conventions();
                conventions.DefiningCommandsAs(type => type.Namespace == "TemplateDomain.PL.Commands");
                conventions.DefiningEventsAs(type => type.Namespace == "TemplateDomain.PL.Events");
            }

            static void SetupAuditing(IConfiguration config, EndpointConfiguration endpointConfiguration)
            {
                var isTurnedOn = config["NSBus:Audit"];
                if (!bool.Parse(isTurnedOn))
                    return;
                endpointConfiguration.AuditProcessedMessagesTo("audit");
            }

            static void SetupHeartBeatAndMetrics(IConfiguration config, EndpointConfiguration endpointConfiguration)
            {
                var isTurnedOn = config["NSBus:HeartbeatAndMetrics"];
                if (!bool.Parse(isTurnedOn))
                    return;

                var svcControlInstanceName = config["NSBus:ServiceControlInstanceName"];
                if (!string.IsNullOrWhiteSpace(svcControlInstanceName))
                {
                    endpointConfiguration.SendHeartbeatTo(
                    serviceControlQueue: svcControlInstanceName,
                    frequency: TimeSpan.FromSeconds(15),
                    timeToLive: TimeSpan.FromSeconds(30));
                }

                var monInstName = config["NSBus:MonitoringInstanceName"];
                if (!string.IsNullOrWhiteSpace(monInstName))
                {
                    var metrics = endpointConfiguration.EnableMetrics();
                    metrics.SendMetricDataToServiceControl(
                        serviceControlMetricsAddress: monInstName,
                        interval: TimeSpan.FromSeconds(10),
                        instanceId: config["NSBus:EndpointName"]
                        );
                }
            }
    }
}