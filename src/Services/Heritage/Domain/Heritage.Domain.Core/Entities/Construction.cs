using Heritage.Domain.Core.Entities.Abstract;
using Heritage.Domain.Core.Entities.Enums;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heritage.Domain.Core.Entities
{
    public class Construction : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public Types? Type { get; set; }
        public Statuses? Status { get; set; }
        [Column(TypeName = "date")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? BuildDate { get; set; }
        [Column(TypeName = "date")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LossDate { get; set; }
        public LossCauses? LossCause { get; set; }
        public Materials? Material { get; set; }
        [Url]
        [MaxLength(2083)]
        public string Article { get; set; }
        public ArchitecturalStyles? ArchitecturalStyle { get; set; }
        public Point Location { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
