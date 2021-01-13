using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;
using Xunit;
using ServiceStack;
using Moq;
using Funq;

namespace TemplateDomain.WebApi.UnitTests.TypeaheadsServiceTests
{
    public class TypeaheadsServiceTests : IClassFixture<TestFixture<TypeaheadQueryService>>
   {
        TypeaheadQueryService Service;

        public TypeaheadsServiceTests(TestFixture<TypeaheadQueryService> f)
        {
            Service = f.Service;
        }

        [Fact]
       public async Task can_execute_request()
       {
           var req = new FilterTypeahead();
           var response = await Service.Any(req) as PaginatedResult<TypeaheadItem>;
           Assert.NotNull(response);
       }
   }

    public class TestFixture<T> : QueryServiceFixtureBase<T> where T : Service
    {
        public override void RegisterServices(Container container)
        {
            container.Register(CreateIOrganizationSearchQueryMock());
        }

            static ITypeaheadSearchQuery CreateIOrganizationSearchQueryMock()
            {
                var queryByIdMock = new Mock<ITypeaheadSearchQuery>();
                queryByIdMock.Setup(x => x.Execute(It.IsAny<ISearchQueryRequest>())).ReturnsAsync(new PaginatedResult<TypeaheadItem>());
                return queryByIdMock.Object;
            }
    }
}