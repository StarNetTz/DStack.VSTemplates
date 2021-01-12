using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public interface ITypeaheadSearchQuery
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(ISearchQueryRequest qry);
    }
}
