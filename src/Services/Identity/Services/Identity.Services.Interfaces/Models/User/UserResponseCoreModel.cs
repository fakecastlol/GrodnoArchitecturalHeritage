using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models.User.Abstract;

namespace Identity.Services.Interfaces.Models.User
{
    public class UserResponseCoreModel : CoreModel
    {
        public string Email { get; set; }

        public Roles Role { get; set; }
    }
}
