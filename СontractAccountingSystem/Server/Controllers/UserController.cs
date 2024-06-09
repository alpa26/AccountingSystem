using MediatR;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Queries.Users.GetUserByName;
using СontractAccountingSystem.Server.Queries.Users.GetUsersList;
using СontractAccountingSystem.Server.Features.Commands.Users.ChangeUser;
using СontractAccountingSystem.Server.Queries.Users.GetUserById;
using System.Security.Claims;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("edit")]
        public async Task GetUserList(UserModel user)
        {
            await _mediator.Send(new ChangeUserCommand(user));
        }

        [HttpPost("getbyname")]
        public async Task<User> GetUserList(string name)
        {
            return await _mediator.Send(new UserByNameQuery(name));
        }

        [HttpPost("getbyid")]
        public async Task<UserModel> GetUserByIdList(Guid id)
        {
            return await _mediator.Send(new UserByIdQuery(id));
        }

        [HttpGet("getrole")]
        public async Task<IActionResult> GetRoleInHeader()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var claimrole = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                HttpContext.Response.Headers.Add("Role", claimrole);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<UserModel>> GetUserList()
        {
            return await _mediator.Send(new UserListQuery());
        }
    }
}
