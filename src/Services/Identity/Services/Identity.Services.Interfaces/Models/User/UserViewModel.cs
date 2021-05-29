using Identity.Services.Interfaces.Models.Filtration;
using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.Sorting;
using System.Collections.Generic;

namespace Identity.Services.Interfaces.Models.User
{
    public class IndexViewModel<T>
        where T : class
    {
        public IEnumerable<UserResponseCoreModel> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
