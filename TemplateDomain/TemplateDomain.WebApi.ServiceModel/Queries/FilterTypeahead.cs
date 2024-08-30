using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel;

[Route("/typeaheads")]
public record FilterTypeahead : PaginatedQueryRequest, IReturn<PaginatedResult<TypeaheadItem>>
{
}
