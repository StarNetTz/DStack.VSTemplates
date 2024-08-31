namespace TemplateDomain.Api.UnitTests;

public class LookupsQueryControllerTests
{
    LookupQueryController Controller;

    public LookupsQueryControllerTests()
    {
        Controller = new LookupQueryController(CreateQueryByIdStub());
    }

    [Fact]
    public async Task Should_GetById()
    {
        var res = await Controller.Get("lookups-timezones");
        Assert.NotNull(res);
    }

    static IQueryById CreateQueryByIdStub()
    {
        var stub = new Mock<IQueryById>();
        stub.Setup(x => x.GetById<Lookup>(It.IsAny<string>())).ReturnsAsync( new Lookup());
        return stub.Object;
    }
}
