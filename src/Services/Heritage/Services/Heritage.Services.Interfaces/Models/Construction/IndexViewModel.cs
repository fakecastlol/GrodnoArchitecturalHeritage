using Heritage.Services.Interfaces.Models.Filtration.FilterSortPagingApp.Models;
using Heritage.Services.Interfaces.Models.Pagination;
using Heritage.Services.Interfaces.Models.Sorting;
using System.Collections.Generic;

namespace Heritage.Services.Interfaces.Models.Construction
{
    public class IndexViewModel<T>
        where T : class
    {
        public IEnumerable<ConstructionResponseCoreModel> Constructions { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
