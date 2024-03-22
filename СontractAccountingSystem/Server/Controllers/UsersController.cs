using MediatR;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Features;
using СontractAccountingSystem.Core.Features.UserCreate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<int> CreateUser(UserCreate.Command cmd)
        {
            return await _mediator.Send(cmd);
        }

        [HttpPost("getbyname")]
        public async Task<User> GetUserList(Core.Features.GetUserByName.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<User[]> GetUserList()
        {
            return await _mediator.Send(new Core.Features.GetUsersList.Query());
        }


    }
}
