using System;
using Heritage.Domain.Core.Entities.Abstract;


namespace Heritage.Domain.Core.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public Guid ConstructionId { get; set; } 

        public virtual Construction Construction { get; set; }
    }
}
