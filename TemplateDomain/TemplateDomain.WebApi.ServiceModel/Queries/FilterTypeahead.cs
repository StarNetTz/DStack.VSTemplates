using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel
{
    [Route("/typeaheads")]
    public class FilterTypeahead : PaginatedQuery, IReturn<PaginatedResult<TypeaheadItem>>
    {
        public string Collection { get; set; }
        public string Qry { get; set; }
    }
}
