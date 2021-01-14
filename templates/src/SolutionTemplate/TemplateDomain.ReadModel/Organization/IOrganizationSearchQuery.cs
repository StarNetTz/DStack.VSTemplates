using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface IOrganizationSearchQuery
    {
        Task<PaginatedResult<Organization>> Execute(ISearchQueryRequest qry);
    }
}
