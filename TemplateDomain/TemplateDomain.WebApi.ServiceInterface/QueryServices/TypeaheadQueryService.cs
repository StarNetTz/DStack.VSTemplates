using TemplateDomain.ReadModel;
using TemplateDomain.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace TemplateDomain.WebApi.ServiceInterface
{
    public class TypeaheadQueryService : Service
    {
        readonly ITypeaheadSearchQuery Query;

        public TypeaheadQueryService(ITypeaheadSearchQuery query)
            => Query = query;

        public async Task<object> Any(FilterTypeahead req)
            => await Query.Execute(req.ConvertTo<SearchQueryRequest>());
    }
}