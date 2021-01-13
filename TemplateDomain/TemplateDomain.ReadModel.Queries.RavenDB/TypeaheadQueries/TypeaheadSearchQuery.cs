using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TemplateDomain.ReadModel.Queries.RavenDB
{
    public class TypeaheadSearchQuery : SearchQuery<TypeaheadItem>, ITypeaheadSearchQuery
    {
        public const string OrganizationsCollectionName = "organizations";
        public TypeaheadSearchQuery(IDocumentStore documentStore) : base(documentStore) { }

        protected override async Task<QueryResult<TypeaheadItem>> SearchAsync(ISearchQueryRequest qry)
        {
            switch (qry.Collection)
            {
                case OrganizationsCollectionName:
                    return await SearchOrganizations(qry);
                default:
                    throw new ArgumentException($"{qry.Collection} not implemented as typeahead search collection!");
            }
        }

        async Task<QueryResult<TypeaheadItem>> SearchOrganizations(ISearchQueryRequest qry)
        {
            QueryResult<TypeaheadItem> retVal = new QueryResult<TypeaheadItem>();
            QueryStatistics statsRef = new QueryStatistics();
            List<TypeaheadItem> searchResult = null;
            using (var ses = DocumentStore.OpenAsyncSession())
            {
                searchResult = await ses.Query<Organization, Organizations_Search>()
                   .Statistics(out statsRef)
                   .Search(x => x.Name, $"{qry.Qry}")
                   .OrderByScoreDescending()
                   .Skip(qry.CurrentPage * qry.PageSize)
                   .Take(qry.PageSize)
                   .Select(x => new TypeaheadItem { Id = x.Id, Value = x.Name })
                   .ToListAsync();

                retVal.Data = searchResult;
                retVal.Statistics = statsRef;
            }
            return retVal;
        }
    }
}