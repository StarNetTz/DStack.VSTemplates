
using Raven.Client.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TemplateDomain.ReadModel.Queries.RavenDB.IntegrationTests
{
    public class TypeaheadSearchQueryTests : IClassFixture<DocumentStoreFixture>
    {
        IDocumentStore DocumentStore;
        IOrganizationQueries OrganizationQueries;

        public TypeaheadSearchQueryTests(DocumentStoreFixture fixture)
        {
            DocumentStore = fixture.DocumentStore;
            OrganizationQueries = new OrganizationQueries(DocumentStore);
        }

        [Fact]
        public async Task Should_Execute()
        {
            var qry = new TypeaheadQueries(OrganizationQueries);
            var res = await qry.Execute(new PaginatedQueryRequest { Qry = new Dictionary<string, string> {
                { TypeaheadConsts.CollectionKey, TypeaheadConsts.OrganizationsCollection },
                { TypeaheadConsts.SearchParamKey, "*" }
            }, CurrentPage = 0, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }
    }
}