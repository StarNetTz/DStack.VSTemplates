using $ext_projectname$.ReadModel;
using ServiceStack;

namespace $safeprojectname$
{
    [Route("/organizations", Verbs = "GET")]
    public class FindOrganizations : PaginatedQuery, IReturn<PaginatedResult<Organization>>
    {
        public string Id { get; set; }
        public string Qry { get; set; }
    }
}
