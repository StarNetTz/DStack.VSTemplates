using $ext_projectname$.ReadModel;
using ServiceStack;

namespace $safeprojectname$
{
    [Route("/typeaheads")]
    public class FilterTypeahead : PaginatedQuery, IReturn<PaginatedResult<TypeaheadItem>>
    {
        public string Collection { get; set; }
        public string Qry { get; set; }
    }
}
