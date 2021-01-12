using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public interface ITypeaheadSmartSearchQuery
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(ISearchQueryRequest qry);
    }
}
