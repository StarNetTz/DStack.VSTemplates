using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceInterface;
using TemplateDomain.WebApi.ServiceModel;
using System.Threading.Tasks;
using Funq;
using ServiceStack;
using Moq;
using Xunit;
using System.Linq;

namespace TemplateDomain.WebApi.UnitTests.LookupsServiceTests
{
    public class LookupsServiceTests : IClassFixture<TestFixture<LookupsService>>
    {
        LookupsService Service;

        public LookupsServiceTests(TestFixture<LookupsService> fixture)
        {
            Service = fixture.Service;
        }

        [Fact]
        public async Task Should_GetLookup()
        {
            var req = new GetLookup();
            var response = await Service.Any(req) as Lookup;
            var doc = response.Data.First();

            Assert.Equal("1", doc.Id);
            Assert.Equal("USA", doc.Value);
        }
    }

    public class TestFixture<T> : QueryServiceFixtureBase<T> where T : Service
    {
        public override void RegisterServices(Container container)
        {
            container.Register(CreateQueryByIdMock());
        }

            static IQueryById CreateQueryByIdMock()
            {
                var queryByIdMock = new Mock<IQueryById>();
                queryByIdMock.Setup(x => x.GetById<Lookup>(It.IsAny<string>())).ReturnsAsync(new Lookup
                {
                    Id = "Lookups-Countries",
                    Data = new System.Collections.Generic.List<LookupItem> {
                    new LookupItem { Id = "1", Value = "USA" }
                }
                });
                return queryByIdMock.Object;
            }
    }
}