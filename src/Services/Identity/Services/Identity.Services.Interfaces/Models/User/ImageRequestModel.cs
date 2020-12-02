using Identity.Services.Interfaces.Models.User.Abstract;
using Microsoft.AspNetCore.Http;

namespace Identity.Services.Interfaces.Models.User
{
    public class ImageRequestModel : CoreModel
    {
        public IFormFile File { get; set; }
    }
}
