using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
using System.Security.Claims;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public class LoginRequestModel
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public class RegRequestModel
        {
            public string Login { get; set; }
            public string FullName { get; set; }
            public string Mail { get; set; }
            public string Password { get; set; }
        }

        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data");
            }

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
        public async Task<IActionResult> Register([FromBody] RegRequestModel request)
        {
            if (request == null)
                return BadRequest("Invalid request data");

            var fullname = request.FullName.Split(' ');
            var user = new User
            {
                UserName = request.Login,
                FirstName = fullname[0],
                SecondName = fullname[1],
                LastName = fullname[2],
                Email = request.Mail
            };
            var res = await _userManager.CreateAsync(user, request.Password);
            if(res.Succeeded)
            {
                return Ok("Succeeded");
            }
            else 
                return BadRequest("NotSucceeded");
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Ok("Succeeded");
        }
    }

}
