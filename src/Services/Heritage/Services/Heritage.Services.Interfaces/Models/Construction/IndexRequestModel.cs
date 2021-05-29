using Heritage.Services.Interfaces.Models.Sorting;
using System;

namespace Heritage.Services.Interfaces.Models.Construction
{
    public class IndexRequestModel
    {
        private int pageNumber;
        private int pageSize;
        private SortState sortOrder;

        public Guid? Construction { get; set; }
        public string Name { get; set; }
        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = 1;
        }

        public int PageSize
        {
            get => pageSize;
            set => pageSize = 10;
        }

        public SortState SortOrder
        {
            get => sortOrder;
            set => sortOrder = SortState.NameAsc;
        }
    }
}
