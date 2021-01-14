﻿using TemplateDomain.WebApi.ServiceInterface;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using System.Threading.Tasks;

namespace TemplateDomain.WebApi.Infrastructure
{
    public class NSBus : IMessageBus
    {
        public async Task Send(object message)
            => await NSBusSingleton.TemplateDomainAppEndpointInstance.Send(message);
    }

    class NSBusSingleton
    {
        public static IEndpointInstance TemplateDomainAppEndpointInstance;

        static NSBusSingleton()
        {
            TemplateDomainAppEndpointInstance = Endpoint.Start(CreateEndpointConfiguration()).GetAwaiter().GetResult();
        }

        static EndpointConfiguration CreateEndpointConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            var endpointConfiguration = new EndpointConfiguration(config["NSBus:EndpointName"]);

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();

            transport.ConnectionString(config["RabbitMQ:ConnectionString"]);

            var routing = transport.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(PL.Commands.RegisterOrganization).Assembly,
                destination: config["NSBus:AppEndpointName"]);

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(
                type =>
                    type.Namespace == "TemplateDomain.PL.Commands"
                );

            endpointConfiguration.SendOnly();
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}