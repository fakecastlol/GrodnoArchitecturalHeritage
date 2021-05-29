using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Models.Sorting;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.ConfirmRegister;
using Identity.Services.Interfaces.Models.User.ForgotPassword;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Profile;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Models.User.Register.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index(Guid? user, string email, int? page, int? pageSize, SortState sortOrder)
        {
            var users = await _userService.GetUsingPaginationAsync(user, email, page ?? 1, pageSize ?? 10, sortOrder);

            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel requestModel)
        {
            //try
            //{
            var result = await _userService.RegisterAsync(requestModel);

            return Ok(result);
            //}
            //catch (ValidationException ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            //}
        }

        [HttpPost("confirmregister")]
        public async Task<IActionResult> ConfirmRegister()
        {
            await _userService.ConfirmRegisterAsync();

            return Ok();
        }

        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmRequestModel model)
        {
            await _userService.ConfirmEmailAsync(model);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel viewModel)
        {
            var loginResponseModel = await _userService.AuthenticateAsync(viewModel);

            if (loginResponseModel == null)
                return BadRequest(new { message = "Username or password is incorrect." });


            return Ok(loginResponseModel);
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestModel model)
        {
            await _userService.ForgotPasswordAsync(model);

            return Ok();
        }

        [HttpPost("role")]
        public async Task<IActionResult> PostRole(SetRoleRequestModel model)
        {
            var setRole = await _userService.SetUserRole(model);
            if (model == null)
                return BadRequest(new { message = "User is not found." });

            return Ok(setRole);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(CoreModel user)
        {
            await _userService.DeleteUserAsync(user.Id);

            return Ok();
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser([FromQuery] CoreModel user)
        {
            var result = await _userService.GetUserByIdAsync(user.Id);

            if (result == null)
                return BadRequest(new { message = "User is not found" });

            return Ok(result);
        }

        [HttpPost("updateprofile")]
        public async Task<IActionResult> PostProfile(ProfileRequestModel model)
        {
            var updateProfile = await _userService.UpdateProfileAsync(model);
            if (model == null)
                return BadRequest(new { message = "User is not found." });

            return Ok(updateProfile);
        }

        [HttpPost("updatepic")]
        public async Task<IActionResult> PostImage([FromForm] ImageViewModel model)
        {
            var updateImage = await _userService.UpdateImageAsync(model);
            if (model == null)
                return BadRequest(new { message = "User is not found" });

            return Ok(updateImage);
        }

        [HttpDelete("deletepic")]
        public async Task<IActionResult> DeleteImage(ImageViewModel model)
        {
            if (model == null)
                return BadRequest(new { message = "User is not found" });

            var deletePic = await _userService.DeleteImageAsync(model);

            return Ok(deletePic);
        }
    }
}
