using Identity.Services.Interfaces.Models.User.Register.Abstract;
using Microsoft.AspNetCore.Http;

namespace Identity.Services.Interfaces.Models.User
{
    public class ImageViewModel : CoreModel
    {
        public IFormFile File { get; set; }
    }
}
