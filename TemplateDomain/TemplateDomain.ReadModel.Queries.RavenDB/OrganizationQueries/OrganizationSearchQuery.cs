using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateDomain.ReadModel.Queries.RavenDB
{
    public class OrganizationSearchQuery : SearchQuery<Organization>, IOrganizationSearchQuery
    {
        public OrganizationSearchQuery(IDocumentStore documentStore) : base(documentStore) { }

        protected override async Task<QueryResult<Organization>> SearchAsync(ISearchQueryRequest qry)
        {
            QueryResult<Organization> retVal = new QueryResult<Organization>();
            QueryStatistics statsRef = new QueryStatistics();
            using (var ses = DocumentStore.OpenAsyncSession())
            {
                var searchResult = await ses.Query<Organization, Organizations_Search>()
                   .Statistics(out statsRef)
                   .Search(x => x.Name, $"{qry.Qry}")
                   .OrderByScoreDescending()
                   .Skip(qry.CurrentPage * qry.PageSize)
                   .Take(qry.PageSize)
                   .ToListAsync();

                retVal.Data = searchResult;
                retVal.Statistics = statsRef;
            }
            return retVal;
        }
    }

    public class Organizations_Search : AbstractMultiMapIndexCreationTask<Organization>
    {
        public Organizations_Search()
        {
            AddMap<Organization>(companies => from c in companies
                                              select new
                                              {
                                                  c.Id,
                                                  c.Name
                                              });

            Index(x => x.Name, FieldIndexing.Search);
        }
    }
}