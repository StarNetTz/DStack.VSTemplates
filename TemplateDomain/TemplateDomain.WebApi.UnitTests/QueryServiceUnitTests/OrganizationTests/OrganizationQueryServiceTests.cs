using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using Funq;
using Moq;
using ServiceStack;
using System.Collections.Generic;

namespace TemplateDomain.WebApi.UnitTests.OrganizationQueryServiceTests
{
    [Collection("AppHost collection")]
    public class OrganizationQueryServiceTests
    {
        ServiceStackHost AppHost;
        OrganizationQueryService Service;

        public OrganizationQueryServiceTests(AppHostFixture fixture)
        {
            AppHost = fixture.AppHost;
            AppHost.Register(CreateQueryByIdMock());
            AppHost.Register(CreateIOrganizationSearchQueryMock());
            Service = AppHost.Container.Resolve<OrganizationQueryService>();
        }

        [Fact]
         public async Task Should_search()
         {
             var response = await Service.Any(new FindOrganizations {  CurrentPage = 0, PageSize = 10, Qry = new Dictionary<string, string> { { QueryKeys.SearchKey, "*" } } }) as PaginatedResult<Organization>;
             Assert.NotNull(response);
         }
   
         [Fact]
         public async Task Should_find_by_id()
         {
             var response = await Service.Any(new FindOrganizations { Qry = new Dictionary<string, string> { { QueryKeys.FindByIdKey, "Organizations-1" } } }) as PaginatedResult<Organization>;
             Assert.NotNull(response.Data);
         }

        static IQueryById CreateQueryByIdMock()
        {
            var queryByIdMock = new Mock<IQueryById>();
            queryByIdMock.Setup(x => x.GetById<Organization>(It.IsAny<string>())).ReturnsAsync(new Organization());
            return queryByIdMock.Object;
        }

        static IOrganizationQueries CreateIOrganizationSearchQueryMock()
        {
            var queryByIdMock = new Mock<IOrganizationQueries>();
            queryByIdMock.Setup(x => x.Execute(It.IsAny<PaginatedQueryRequest>())).ReturnsAsync(new PaginatedResult<Organization>());
            return queryByIdMock.Object;
        }
    }
}