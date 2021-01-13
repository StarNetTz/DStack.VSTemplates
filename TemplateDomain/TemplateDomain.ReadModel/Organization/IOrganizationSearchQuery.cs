using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public interface IOrganizationSearchQuery
    {
        Task<PaginatedResult<Organization>> Execute(ISearchQueryRequest qry);
    }
}
