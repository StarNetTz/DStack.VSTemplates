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

        public PaginatedResult()
        {
            Data = new List<T>();
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