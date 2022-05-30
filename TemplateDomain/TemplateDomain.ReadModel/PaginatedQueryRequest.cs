using System.Collections.Generic;

namespace TemplateDomain.ReadModel
{
    public class PaginatedQueryRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } = 1;
        public Dictionary<string, string> Qry { get; set; }

        public PaginatedQueryRequest()
        {
            Qry = new Dictionary<string, string>();
        }
    }
}