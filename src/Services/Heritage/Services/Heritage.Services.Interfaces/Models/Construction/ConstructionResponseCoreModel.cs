using System;
using Heritage.Domain.Core.Entities.Enums;
using Heritage.Services.Interfaces.Models.Construction.Abstract;

namespace Heritage.Services.Interfaces.Models.Construction
{
    public class ConstructionResponseCoreModel : CoreModel
    {
        public string Name { get; set; }
        public Types Type { get; set; }
        public DateTime BuildDate { get; set; }
        public DateTime LossDate { get; set; }
        public LossCauses LossCause { get; set; }
        public Materials Material { get; set; }
        public string Article { get; set; }
        public ArchitecturalStyles ArchitecturalStyle { get; set; }
        //public Point Location { get; set; }
        public string Address { get; set; }
    }
}