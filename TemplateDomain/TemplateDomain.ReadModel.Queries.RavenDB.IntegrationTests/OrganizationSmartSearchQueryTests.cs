using Raven.Client.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TemplateDomain.ReadModel.Queries.RavenDB.IntegrationTests
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
            var qry = new OrganizationQueries(DocumentStore);
            var res = await qry.Execute(new PaginatedQueryRequest { Qry = new Dictionary<string, string> { { QueriesKeys.SearchKey,"*"} }, CurrentPage = 0, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }
        
        [Fact]
        public async Task OverflownQuery_Should_Return_FirstPage()
        {
            var qry = new OrganizationQueries(DocumentStore);
            var res = await qry.Execute(new PaginatedQueryRequest { Qry = new Dictionary<string, string> { { QueriesKeys.SearchKey, "*" } }, CurrentPage = 100, PageSize = 10 });
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