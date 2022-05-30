using ServiceStack;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using Xunit;

namespace TemplateDomain.WebApi.UnitTests.HelloServiceUnitTest
{
    [Collection("AppHost collection")]
    public class HelloServiceUnitTest
    {
        ServiceStackHost AppHost;

        public HelloServiceUnitTest(AppHostFixture fixture)
        {
            AppHost = fixture.AppHost;
            AppHost.Container.AddTransient<MyServices>();
        }


        [Fact]
        public void Can_call_MyServices()
        {
            var service = AppHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.Equal("Hello, World!", response.Result);
        }
    }
}
