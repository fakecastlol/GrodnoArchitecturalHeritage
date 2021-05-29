using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Heritage.Domain.Core.Entities.Abstract;


namespace Heritage.Domain.Core.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [ForeignKey("ConstructionId")]
        public Guid ConstructionId { get; set; }
        public Construction Construction { get; set; }
    }
}
