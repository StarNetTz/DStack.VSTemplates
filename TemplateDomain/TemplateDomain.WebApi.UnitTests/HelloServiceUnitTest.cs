namespace TemplateDomain.WebApi.UnitTests.HelloServiceUnitTest;

[Collection("AppHost collection")]
public class HelloServiceUnitTest
{
    ServiceStackHost AppHost;

    public HelloServiceUnitTest(AppHostFixture fixture)
    {
        AppHost = fixture.AppHost;
        AppHost.Container.AddTransient<HelloServices>();
    }

    [Fact]
    public void Should_say_hello()
    {
        var service = AppHost.Container.Resolve<HelloServices>();

        var response = (HelloResponse)service.Any(new Hello { Name = "World" });

        Assert.Equal("Hello, World!", response.Result);
    }
}