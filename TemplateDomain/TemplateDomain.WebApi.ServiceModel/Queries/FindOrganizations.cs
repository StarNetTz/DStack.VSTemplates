using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel
{
    [Route("/organizations", Verbs = "GET")]
    public class FindOrganizations : PaginatedQueryRequest, IReturn<PaginatedResult<Organization>>
    {
    }
}
