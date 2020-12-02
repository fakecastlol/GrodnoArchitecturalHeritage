using System;
using Heritage.Domain.Core.Entities.Abstract;
using Heritage.Domain.Core.Entities.Enums;

namespace Heritage.Domain.Core.Entities
{
    public class Сonstruction : BaseEntity
    {
        public Types Type { get; set; }
        public DateTime BuildDate { get; set; }
        public Materials Material { get; set; }

        
    }
}
