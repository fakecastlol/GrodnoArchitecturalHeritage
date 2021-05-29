using Heritage.Domain.Core.Entities.Enums;
using Heritage.Services.Interfaces.Models.Construction.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Heritage.Services.Interfaces.Models.Construction
{
    public class ConstructionRequestCoreModel : CoreModel
    {
        [Required(ErrorMessage = "Name not specified")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Name should be minimum 2 characters and a maximum of 100 characters")]
        public string Name { get; set; }
        public Types? Type { get; set; }
        public Statuses? Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BuildDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LossDate { get; set; }
        public LossCauses? LossCause { get; set; }
        public Materials? Material { get; set; }
        [DataType(DataType.Url)]
        [StringLength(2083, ErrorMessage = "Url should be a maximum of 2083 characters")]
        public string Article { get; set; }
        public ArchitecturalStyles? ArchitecturalStyle { get; set; }
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Address should be a maximum of 200 characters")]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        [StringLength(300, ErrorMessage = "Description should be a maximum of 300 characters")]
        public string Description { get; set; }
    }
}