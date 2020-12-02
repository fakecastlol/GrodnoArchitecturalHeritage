using Identity.Domain.Core.Entities.Enums;
using Identity.Services.Interfaces.Models.User.Abstract;

namespace Identity.Services.Interfaces.Models.User
{
    public class SetRoleRequestModel : CoreModel
    {
        public Roles Role { get; set; }
    }
}
