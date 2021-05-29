using System;

namespace Identity.Services.Interfaces.Models.Pagination
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int Count { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            Count = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

        //public static PageViewModel<T> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    var count = source.Count();
        //    var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    return new PageViewModel<T>(items, count, pageIndex, pageSize);
        //}
    }
}
