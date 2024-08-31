namespace TemplateDomain.WebApi.UnitTests.LookupsServiceTests;

[Collection("AppHost collection")]
public class LookupsServiceTests
{
    ServiceStackHost AppHost;

    public LookupsServiceTests(AppHostFixture fixture)
    {
        AppHost = fixture.AppHost;
        AppHost.Container.Register(CreateQueryByIdMock());
    }

    [Fact]
    public async Task Should_GetLookup()
    {
        var req = new GetLookup();
        var service = AppHost.Container.Resolve<LookupService>();
        var response = await service.Any(req) as Lookup;
        var doc = response.Data.First();

        Assert.Equal("1", doc.Id);
        Assert.Equal("USA", doc.Val);
    }

    static IQueryById CreateQueryByIdMock()
    {
        var queryByIdMock = new Mock<IQueryById>();
        queryByIdMock.Setup(x => x.GetById<Lookup>(It.IsAny<string>())).ReturnsAsync(new Lookup
        {
            Id = "Lookups-Countries",
            Data = new List<Ref> {
                new Ref { Id = "1", Val = "USA" }
            }
        });
        return queryByIdMock.Object;
    }
}