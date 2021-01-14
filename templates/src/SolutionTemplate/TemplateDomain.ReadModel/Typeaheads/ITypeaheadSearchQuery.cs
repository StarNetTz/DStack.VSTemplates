using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface ITypeaheadSearchQuery
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(ISearchQueryRequest qry);
    }
}
