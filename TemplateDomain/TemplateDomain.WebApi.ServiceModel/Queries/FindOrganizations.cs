using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel;

[Route("/organizations", Verbs = "GET")]
public record FindOrganizations : PaginatedQueryRequest, IReturn<PaginatedResult<Organization>>
{
}
