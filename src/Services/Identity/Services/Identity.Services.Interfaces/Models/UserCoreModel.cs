using Identity.Services.Interfaces.Models.Abstract;

namespace Identity.Services.Interfaces.Models
{
    public class UserCoreModel : CoreModel
    {
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Token { get; set; }

        public RoleCoreModel Role { get; set; }
    }
}
