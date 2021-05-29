using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Image;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Heritage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("addimage")]
        public async Task<IActionResult> PostImage([FromForm] ImageRequestModel model)
        {
            var addImage = await _imageService.AddImageAsync(model);
            if (model == null)
                return BadRequest(new { message = "Image is not found" });

            return Ok(addImage);
        }
    }
}
