using Identity.Services.Interfaces.Models.User;
using System.Collections.Generic;

namespace Identity.Services.Interfaces.Models.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<UserResponseCoreModel> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
