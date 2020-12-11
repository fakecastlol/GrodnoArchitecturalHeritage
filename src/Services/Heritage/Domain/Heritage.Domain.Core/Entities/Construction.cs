using Heritage.Domain.Core.Entities.Abstract;
using Heritage.Domain.Core.Entities.Enums;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace Heritage.Domain.Core.Entities
{
    public class Construction : BaseEntity
    {
        public Construction()
        {
            Images = new HashSet<Image>();
        }

        public string Name { get; set; }
        public Types Type { get; set; }
        public DateTime BuildDate { get; set; }
        public DateTime LossDate { get; set; }
        public LossCauses LossCause { get; set; }
        public Materials Material { get; set; }
        public string Article { get; set; }
        public ArchitecturalStyles ArchitecturalStyle { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Image> Images { get; set; } /*= new List<Image>();*/
    }
}
