
using Raven.Client.Documents;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TemplateDomain.ReadModel.Queries.RavenDB.IntegrationTests
{
    public class TypeaheadSearchQueryTests : IClassFixture<DocumentStoreFixture>
    {
        IDocumentStore DocumentStore;

        public TypeaheadSearchQueryTests(DocumentStoreFixture fixture)
        {
            DocumentStore = fixture.DocumentStore;
        }

        [Fact]
        public async Task Should_Execute()
        {
            var qry = new TypeaheadSearchQuery(DocumentStore);
            var res = await qry.Execute(new SearchQueryRequest { Collection = TypeaheadSearchQuery.OrganizationsCollectionName, Qry = "*", CurrentPage = 0, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }

        [Fact]
        public async Task Usupported_Collection_Throws_ArgumentException()
        {
            var qry = new TypeaheadSearchQuery(DocumentStore);
            await Assert.ThrowsAsync<ArgumentException>(async () => await qry.Execute(new SearchQueryRequest { Collection = "unsupported", Qry = "*", CurrentPage = 0, PageSize = 10 }));
        }

        [Fact]
        public async Task OverflownQueryReturnsFirstPage()
        {
            var qry = new OrganizationSearchQuery(DocumentStore);
            var res = await qry.Execute(new SearchQueryRequest { Qry = "*", CurrentPage = 100, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }

        [Fact]
        public async Task CanGetOrganizationById()
        {
            var q = new QueryById(DocumentStore);
            var cmp = await q.GetById<Organization>("Organizations-1");
            Assert.Equal("Slime Ltd", cmp.Name);
        }
    }
}