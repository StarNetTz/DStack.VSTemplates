using ServiceStack;
using ServiceStack.Testing;
using System;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using Xunit;

namespace TemplateDomain.WebApi.UnitTests.HelloServiceUnitTest
{
    public class HelloServiceUnitTest : IClassFixture<TestFixture>
    {
        ServiceStackHost AppHost;

        public HelloServiceUnitTest(TestFixture fixture)
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

    public class TestFixture : IDisposable
    {
        public ServiceStackHost AppHost;

        public TestFixture()
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
