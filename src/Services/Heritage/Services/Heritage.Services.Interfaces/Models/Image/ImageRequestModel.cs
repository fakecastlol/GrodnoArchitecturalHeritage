using Heritage.Services.Interfaces.Models.Construction.Abstract;
using Microsoft.AspNetCore.Http;
using System;

namespace Heritage.Services.Interfaces.Models.Image
{
    public class ImageRequestModel : CoreModel
    {
        public IFormFile File { get; set; }

        public Guid ConstructionId { get; set; }
        //public ImageCoreModel ImageModel { get; set; }
    }
}
