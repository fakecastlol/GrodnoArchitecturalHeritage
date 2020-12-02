using Identity.Domain.Core.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Domain.Core.Entities.Enums;

namespace Identity.Domain.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime RegistrationDate { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime LastVisited { get; set; }
        public string Location { get; set; }
        public int ArticleCount { get; set; }
        public int MessagesCount { get; set; }
        public int Rank { get; set; }
        public string Avatar { get; set; }
    }
}
