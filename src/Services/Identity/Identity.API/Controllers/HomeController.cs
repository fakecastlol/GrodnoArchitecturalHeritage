using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Content("index page");
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return Content("login only for administrator");
        }
    }
}
