using System.Collections.Generic;

namespace TemplateDomain.ReadModel
{
    public interface IPaginatedResult
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }

    public class PaginatedResult<T> : IPaginatedResult
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public static PaginatedResult<TypeaheadItem> CreateFrom(IPaginatedResult src, List<TypeaheadItem> data)
        {
            return new PaginatedResult<TypeaheadItem>
            {
                CurrentPage = src.CurrentPage,
                PageSize = src.PageSize,
                TotalItems = src.TotalItems,
                TotalPages = src.TotalPages,
                Data = data
            };
        }
    }

    
}
