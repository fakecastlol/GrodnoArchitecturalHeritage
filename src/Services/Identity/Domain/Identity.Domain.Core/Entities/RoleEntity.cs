using Identity.Domain.Core.Entities.Abstract;
using System.Collections.Generic;

namespace Identity.Domain.Core.Entities
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public List<UserEntity> Users { get; set; }

        public RoleEntity()
        {
            Users = new List<UserEntity>();
        }
    }
}
