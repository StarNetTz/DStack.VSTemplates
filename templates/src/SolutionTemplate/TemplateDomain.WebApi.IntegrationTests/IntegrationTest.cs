using Funq;
using ServiceStack;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using System;
using Xunit;

namespace $safeprojectname$
{
    public class IntegrationTest : IDisposable
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost Host;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(MyServices).Assembly) { }

            public override void Configure(Container container)
            {
            }
        }

        public IntegrationTest()
        {
            Host = new AppHost()
                .Init()
                .Start(BaseUri);
        }


        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Fact]
        public void Can_Call_Hello_Service()
        {
            var client = CreateClient();

            var response = client.Get(new Hello { Name = "World" });

            Assert.Equal("Hello, World!", response.Result);
        }

        public void Dispose()
        {
            Host.Dispose();
        }
    }
}