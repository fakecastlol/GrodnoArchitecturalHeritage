using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models.Abstract;

namespace Identity.Services.Interfaces.Models
{
    public class UserResponseCoreModel : CoreModel
    {
        public string Email { get; set; }

        public Roles Role { get; set; }
    }
}
