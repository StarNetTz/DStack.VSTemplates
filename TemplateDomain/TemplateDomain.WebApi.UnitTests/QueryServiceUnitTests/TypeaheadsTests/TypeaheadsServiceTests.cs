using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using ServiceStack;
using Moq;
using System.Collections.Generic;

namespace TemplateDomain.WebApi.UnitTests.TypeaheadsServiceTests;

[Collection("AppHost collection")]
public class TypeaheadsServiceTests
{
    ServiceStackHost AppHost;

    public TypeaheadsServiceTests(AppHostFixture fixture)
    {
        AppHost = fixture.AppHost;
        AppHost.Container.Register(CreateIOrganizationSearchQueryMock());
    }

    [Fact]
    public async Task Should_execute()
    {
        var service = AppHost.Container.Resolve<TypeaheadQueryService>();
        var req = new FilterTypeahead();
        var response = await service.Any(req) as PaginatedResult<TypeaheadItem>;
        Assert.NotNull(response);
    }

        static ITypeaheadQueries CreateIOrganizationSearchQueryMock()
        {
            var queryByIdMock = new Mock<ITypeaheadQueries>();
            queryByIdMock.Setup(x => x.Execute(It.IsAny<PaginatedQueryRequest>())).ReturnsAsync(new PaginatedResult<TypeaheadItem>());
            return queryByIdMock.Object;
        }
}