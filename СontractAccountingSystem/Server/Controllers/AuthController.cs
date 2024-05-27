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

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (request == null)
                return BadRequest("Invalid request data");

            var user = await _userManager.FindByNameAsync(request.Login);
            if (user is null)
                BadRequest("User Not Found"); 
            
            if (await _userManager.CheckPasswordAsync(user, request.Password)) {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name , user.UserName) };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));
                return Ok("Request successfully processed");

            }
            return BadRequest("Wrond password");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel request)
        {
            if (request == null)
                return BadRequest("Invalid request data");

            var res = await _mediator.Send(new UserCreateCommand(request));

            if (res.Success)
                return Ok("Succeeded");
            else
                return BadRequest(res.Message);
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Ok("Succeeded");
        }
    }

    public class LoginRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
