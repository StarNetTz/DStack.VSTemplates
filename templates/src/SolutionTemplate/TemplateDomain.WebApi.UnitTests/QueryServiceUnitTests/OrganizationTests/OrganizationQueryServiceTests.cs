using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using Funq;
using Moq;
using ServiceStack;

namespace $safeprojectname$.OrganizationQueryServiceTests
{
    public class OrganizationQueryServiceTests : IClassFixture<TestFixture<OrganizationQueryService>>
    {
        OrganizationQueryService Service;

        public OrganizationQueryServiceTests(TestFixture<OrganizationQueryService> fixture)
        {
            Service = fixture.Service;
        }
        [Fact]
         public async Task Should_Search()
         {
             var response = await Service.Any(new FindOrganizations { CurrentPage = 0, PageSize = 10, Qry = "*" }) as PaginatedResult<Organization>;
             Assert.NotNull(response);
         }
   
         [Fact]
         public async Task can_get_by_id()
         {
             var response = await Service.Any(new FindOrganizations { Id = "Organizations-1" }) as PaginatedResult<Organization>;
             Assert.NotNull(response.Data);
         }
    }

    public class TestFixture<T> : QueryServiceFixtureBase<T> where T : Service
    {
        public override void RegisterServices(Container container)
        {
            container.Register(CreateQueryByIdMock());
            container.Register(CreateIOrganizationSearchQueryMock());
        }

            static IQueryById CreateQueryByIdMock()
            {
                var queryByIdMock = new Mock<IQueryById>();
                queryByIdMock.Setup(x => x.GetById<Organization>(It.IsAny<string>())).ReturnsAsync(new Organization());
                return queryByIdMock.Object;
            }

            static IOrganizationSearchQuery CreateIOrganizationSearchQueryMock()
            {
                var queryByIdMock = new Mock<IOrganizationSearchQuery>();
                queryByIdMock.Setup(x => x.Execute(It.IsAny<ISearchQueryRequest>())).ReturnsAsync(new PaginatedResult<Organization>());
                return queryByIdMock.Object;
            }
    }
}