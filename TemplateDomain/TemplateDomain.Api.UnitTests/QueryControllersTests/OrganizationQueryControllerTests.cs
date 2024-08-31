using TemplateDomain.Testing.ReadModel;

namespace TemplateDomain.Api.UnitTests;

public class OrganizationQueryControllerTests
{
    OrganizationQueryController Controller;

    public OrganizationQueryControllerTests()
    {
        Controller = new OrganizationQueryController(CreateOrganizationQueriesStub(), CreateQueryByIdStub());
    }

    [Fact]
    public async Task Should_GetById()
    {
        var res = await Controller.Find(new PaginatedQueryRequest { Qry = new Dictionary<string, string> { { QueryKeys.FindByIdKey,"" } } });
        Assert.NotNull(res);
    }

    [Fact]
    public async Task Should_Execute_Query()
    {
        var res = await Controller.Find(new PaginatedQueryRequest() );
        Assert.NotNull(res);
    }

    static IOrganizationQueries CreateOrganizationQueriesStub()
    {
        var stub = new Mock<IOrganizationQueries>();
        stub.Setup(x => x.Execute(It.IsAny<PaginatedQueryRequest>())).ReturnsAsync(new PaginatedResult<Organization>());
        return stub.Object;
    }

    static IQueryById CreateQueryByIdStub()
    {
        var stub = new Mock<IQueryById>();
        stub.Setup(x => x.GetById<Organization>(It.IsAny<string>())).ReturnsAsync( (string id) =>  OrganizationTestData.CreateDefault(id));
        return stub.Object;
    }
}
