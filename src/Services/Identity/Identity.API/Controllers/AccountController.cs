using AutoMapper;
using Identity.API.Models;
using Identity.API.Models.AppViewModel;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Models;
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var map = _mapper.Map<RegisterCoreModel>(model);

                var result = await _userService.CreateAsync(map);

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
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<LoginCoreModel>(viewModel);

                //if (user != null)
                //{
                //    return Ok();
                //}
                //ModelState.AddModelError("", "Incorrect login or password");

                var userCoreModel = await _userService.Authenticate(user);

                var result = _mapper.Map<UserViewModel>(userCoreModel);

                return Ok(result);
                //await _userService.IsUserExistAsync(user);

            }
            return BadRequest(viewModel);
        }

        //private async Task Authenticate(UserViewModel user)
        //{
        //    // create one claim
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
        //        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
        //    };
        //    // create object ClaimsIdentity
        //    ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
        //        ClaimsIdentity.DefaultRoleClaimType);
        //    // set authenticated cooks
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        //}
    }
}
