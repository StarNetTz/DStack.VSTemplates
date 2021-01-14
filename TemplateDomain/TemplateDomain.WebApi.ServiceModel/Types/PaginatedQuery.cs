namespace TemplateDomain.WebApi.ServiceModel
{
    public class PaginatedQuery
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } = 1;
    }
}
