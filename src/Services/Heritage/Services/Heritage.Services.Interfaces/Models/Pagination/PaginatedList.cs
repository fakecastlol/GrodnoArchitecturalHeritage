using System;
using System.Collections.Generic;
using System.Linq;

namespace Heritage.Services.Interfaces.Models.Pagination
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int Count { get; private set; }

        public List<T> ItemList { get; private set; }

        public PaginatedList()
        {
            ItemList = new List<T>();
        }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Count = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            ItemList = new List<T>();
            ItemList.AddRange(items);
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

        public static PaginatedList<T> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}