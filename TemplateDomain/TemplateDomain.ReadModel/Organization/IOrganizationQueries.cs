using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public class OrganizationQueriesKeys
    {
        public const string FindByIdParamKey = "byId";
        public const string SearchKey = "search";
    }

    public interface IOrganizationQueries
    {
        Task<PaginatedResult<Organization>> Execute(PaginatedQueryRequest qry);
    }
}
