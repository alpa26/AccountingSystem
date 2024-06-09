using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.Commands.Users.UserRegister;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Queries.Users.GetUserByName;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, IMediator mediator, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (request == null)
                return BadRequest("Invalid request data");
            var user = await _mediator.Send(new UserByNameQuery(request.Login));
            if (user is null)
                return BadRequest("User Not Found");
            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>(), "Cookies")));
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    HttpContext.Response.Headers.Add("Role", user.Role.Name);
                    return Ok();
                }
                else HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex) { }
            return BadRequest("Wrond password");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel request)
        {
            if (request == null)
                return BadRequest("Invalid request data");

            var res = await _mediator.Send(new UserCreateCommand(request));

            if (res.Success)
                return Ok();
            else
                return BadRequest(res.Message);
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Ok();
        }
    }

    public class LoginRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
