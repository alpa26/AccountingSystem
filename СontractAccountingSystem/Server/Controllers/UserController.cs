using MediatR;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Queries.Users.GetUserByName;
using СontractAccountingSystem.Server.Commands.Users.UserCreate;
using СontractAccountingSystem.Server.Queries.Users.GetUsersList;
using СontractAccountingSystem.Server.Queries.Roles.GetRoleList;
using СontractAccountingSystem.Server.Features.Queries.Users.GetWorkerList;

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

        [HttpPost("create")]
        public async Task<Guid> CreateUser(UserCreateCommand cmd)
        {
            return await _mediator.Send(cmd);
        }

        [HttpPost("getbyname")]
        public async Task<User> GetUserList(UserByNameQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<User[]> GetUserList()
        {
            return await _mediator.Send(new UserListQuery());
        }

        [HttpGet("workerlist")]
        public async Task<List<PersonModel>> GetWorkerList()
        {
            return await _mediator.Send(new WorkerListQuery());
        }
    }
}
