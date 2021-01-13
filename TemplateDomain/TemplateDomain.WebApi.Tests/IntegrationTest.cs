using Funq;
using ServiceStack;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System;
using Xunit;

namespace TemplateDomain.WebApi.Tests
{
    public class IntegrationTest : IDisposable
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(MyServices).Assembly) { }

            public override void Configure(Container container)
            {
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }


        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Fact]
        public void Can_call_Hello_Service()
        {
            var client = CreateClient();

            var response = client.Get(new Hello { Name = "World" });

            Assert.Equal("Hello, World!", response.Result);
        }

        public void Dispose()
        {
            appHost.Dispose();
        }
    }
}