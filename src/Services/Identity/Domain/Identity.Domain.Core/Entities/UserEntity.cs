using System;
using System.Text.Json.Serialization;
using Identity.Domain.Core.Entities.Abstract;

namespace Identity.Domain.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Guid? RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
