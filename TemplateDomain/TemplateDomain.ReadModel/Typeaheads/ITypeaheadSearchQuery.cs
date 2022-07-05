using System.Threading.Tasks;

namespace TemplateDomain.ReadModel
{
    public interface ITypeaheadQueries
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(PaginatedQueryRequest qry);
    }

    public interface ITypeaheadQuery
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(PaginatedQueryRequest qry);
    }
}
