internal class EndpointConfigurationFactory
{
    public static EndpointConfiguration Create(IConfiguration config)
    {
        var endpointConfiguration = new EndpointConfiguration(config["NSBus:EndpointName"]);
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();


        endpointConfiguration.LicensePath("config/license.xml");

        InitializeTransport(config, endpointConfiguration);

        SetupConventions(endpointConfiguration);

        endpointConfiguration.EnableInstallers();
        return endpointConfiguration;
    }

    static void InitializeTransport(IConfiguration config, EndpointConfiguration endpointConfiguration)
    {
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseConventionalRoutingTopology(QueueType.Classic);
        transport.ConnectionString(config["RabbitMQ:ConnectionString"]);
        SetupRouting(transport, config);
    }

    static void SetupRouting(TransportExtensions<RabbitMQTransport> transport, IConfiguration config)
    {
        var routing = transport.Routing();

        routing.RouteToEndpoint(
        assembly: typeof(TemplateDomain.PL.Commands.RegisterOrganization).Assembly,
        destination: config["NSBus:AppEndpointName"]);
    }

    static void SetupConventions(EndpointConfiguration endpointConfiguration)
    {
        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "TemplateDomain.PL.Commands");
        conventions.DefiningEventsAs(type => type.Namespace == "TemplateDomain.PL.Events");
        conventions.DefiningMessagesAs(type => type.Namespace == "TemplateDomain.PL.Messages");
    }
}