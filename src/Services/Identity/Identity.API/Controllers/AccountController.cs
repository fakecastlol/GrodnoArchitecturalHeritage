using AutoMapper;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Abstract;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet("pusers")]
        public async Task<IActionResult> Index(int? page, int? pageSize)
        {
            var users = await _userService.GetUsingPaginationAsync(page ?? 1, pageSize ?? 10);

            return Ok(users);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            try
            {
                var registerCoreModel = _mapper.Map<RegisterCoreModel>(model);

                var result = await _userService.RegisterAsync(registerCoreModel);

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel viewModel)
        {
            var loginResponseModel = await _userService.AuthenticateAsync(viewModel);

            if (loginResponseModel == null)
                return BadRequest(new {message = "Username or password is incorrect."});


            return Ok(loginResponseModel);
        }

        //[HttpGet("role")]
        //public async Task<IActionResult> SetRole(Guid id)
        //{
        //    var user = await _userService.GetUserByIdAsync(id);

        //    if (user == null)
        //        return BadRequest(new {message = "User is not found."});

        //    return Ok(user);
        //}

        [HttpPost("role")]
        public async Task<IActionResult> SetRole(SetRoleRequestModel model)
        {
            var setRole = await _userService.SetUserRole(model);
            if (model == null)
                return BadRequest(new { message = "User is not found." });

            return Ok(setRole);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(CoreModel user)
        {
            await _userService.DeleteAsync(user.Id);

            return Ok();
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser([FromQuery]CoreModel user)
        {
            var result = await _userService.GetUserByIdAsync(user.Id);

            return Ok(result);
        }

    }
}
