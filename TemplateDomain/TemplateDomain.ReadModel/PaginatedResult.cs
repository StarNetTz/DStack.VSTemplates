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

        public static PaginatedResult<T> CreateFromSingleItem(T item)
        {
            return new PaginatedResult<T>
            {
                CurrentPage = 0,
                PageSize = 1,
                TotalItems = 1,
                TotalPages = 1,
                Data = new List<T>() {item}
            };
        }
    }
}