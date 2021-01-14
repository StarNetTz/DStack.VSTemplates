using Raven.Client.Documents;
using System.Threading.Tasks;
using Xunit;

namespace $safeprojectname$
{
    public class OrganizationSearchQueryTests : IClassFixture<DocumentStoreFixture>
    {
        IDocumentStore DocumentStore;

        public OrganizationSearchQueryTests(DocumentStoreFixture fixture)
        {
            DocumentStore = fixture.DocumentStore;
        }

        [Fact]
        public async Task Should_Execute()
        {
            var qry = new OrganizationSearchQuery(DocumentStore);
            var res = await qry.Execute(new SearchQueryRequest { Qry = "*", CurrentPage = 0, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }

        [Fact]
        public async Task OverflownQuery_Should_Return_FirstPage()
        {
            var qry = new OrganizationSearchQuery(DocumentStore);
            var res = await qry.Execute(new SearchQueryRequest { Qry = "*", CurrentPage = 100, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }

        [Fact]
        public async Task Should_GetById()
        {
            var q = new QueryById(DocumentStore);
            var cmp = await q.GetById<Organization>("Organizations-1");
            Assert.Equal("Slime Ltd", cmp.Name);
        }
    }
}