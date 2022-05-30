
using Raven.Client.Documents;
using System;
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
        public async Task Should_execute()
        {
            var qry = new TypeaheadQueries(OrganizationQueries);
            var res = await qry.Execute(new PaginatedQueryRequest { Qry = new Dictionary<string, string> {
                { TypeaheadConsts.CollectionKey, TypeaheadConsts.OrganizationsCollection },
                { TypeaheadConsts.SearchParamKey, "*" }
            }, CurrentPage = 0, PageSize = 10 });
            Assert.Equal(2, res.Data.Count);
        }

        [Fact]
        public async Task Should_throw_not_implemented_on_non_existant_collection()
        {
            var qry = new TypeaheadQueries(OrganizationQueries);
            await Assert.ThrowsAsync<NotImplementedException>(async () => 
                await qry.Execute(new PaginatedQueryRequest
                {
                    Qry = new Dictionary<string, string> {
                        { TypeaheadConsts.CollectionKey, "none" },
                        { TypeaheadConsts.SearchParamKey, "*" }
                    },
                    CurrentPage = 0,
                    PageSize = 10
                })
            );
        }

        [Fact]
        public async Task Should_throw_not_implemented_on_no_collection_key()
        {
            var qry = new TypeaheadQueries(OrganizationQueries);
            await Assert.ThrowsAsync<NotImplementedException>(async () => 
                await qry.Execute(new PaginatedQueryRequest
                {
                    CurrentPage = 0,
                    PageSize = 10
                })
            );
        }
    }
}