using Identity.Domain.Core.Entities.Abstract;
using Identity.Domain.Core.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Core.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        [MaxLength(30)]
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(72)]
        public string Password { get; set; }
        public Roles Role { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string Login { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? RegistrationDate { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? LastVisited { get; set; }
        [MaxLength(20)]
        public string Location { get; set; }
        public int ArticleCount { get; set; }
        public int MessagesCount { get; set; }
        public int Rank { get; set; }
        [MaxLength(72)]
        public string Avatar { get; set; }
    }
}
