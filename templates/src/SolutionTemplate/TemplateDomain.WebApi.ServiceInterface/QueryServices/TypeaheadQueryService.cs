using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace $safeprojectname$
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