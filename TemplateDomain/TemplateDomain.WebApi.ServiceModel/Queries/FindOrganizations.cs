using TemplateDomain.ReadModel;
using ServiceStack;

namespace TemplateDomain.WebApi.ServiceModel
{
    [Route("/organizations", Verbs = "GET")]
    public class FindOrganizations : PaginatedQuery, IReturn<PaginatedResult<Organization>>
    {
        public string Id { get; set; }
        public string Qry { get; set; }
    }
}
