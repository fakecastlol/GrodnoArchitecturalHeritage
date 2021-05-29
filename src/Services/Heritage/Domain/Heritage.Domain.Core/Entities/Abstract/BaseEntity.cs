using System;
using System.ComponentModel.DataAnnotations;

namespace Heritage.Domain.Core.Entities.Abstract
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
