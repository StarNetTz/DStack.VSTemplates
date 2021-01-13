using ServiceStack;
using ServiceStack.Testing;
using System;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using Xunit;

namespace TemplateDomain.WebApi.Tests
{
    public class UnitTest : IClassFixture<UnitTestFixture>
    {
        ServiceStackHost AppHost;

        public UnitTest(UnitTestFixture fixture)
        {
            AppHost = fixture.AppHost;
        }

        [Fact]
        public void Can_call_MyServices()
        {
            var service = AppHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.Equal("Hello, World!", response.Result);
        }
    }

    public class UnitTestFixture : IDisposable
    {
        public ServiceStackHost AppHost;

        public UnitTestFixture()
        {
            AppHost = new BasicAppHost().Init();
            AppHost.Container.AddTransient<MyServices>();
        }

        public void Dispose()
        {
            AppHost.Dispose();
        }
    }
}
