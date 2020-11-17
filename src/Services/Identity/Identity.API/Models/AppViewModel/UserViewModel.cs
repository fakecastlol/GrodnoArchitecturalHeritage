using System.Security.Policy;
using Identity.API.Models.AppViewModel.Abstract;

namespace Identity.API.Models.AppViewModel
{
    public class UserViewModel : BaseViewModel
    {
        //public string Email { get; set; }
        //public string Password { get; set; }
        public string Token { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
