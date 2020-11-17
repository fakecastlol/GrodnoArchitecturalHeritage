using Identity.Domain.Core.Entities.Abstract;

namespace Identity.Domain.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
