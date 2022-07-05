using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel
{
    [Route("/typeaheads")]
    public class FilterTypeahead : PaginatedQueryRequest, IReturn<PaginatedResult<TypeaheadItem>>
    {
    }
}
