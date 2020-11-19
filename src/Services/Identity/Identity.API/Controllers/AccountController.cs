using AutoMapper;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        [HttpGet("users")]
        public async Task<IActionResult> Register()
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
    }
}
