using MediatR;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Server.Features;
using СontractAccountingSystem.Server.Features.UserCreate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Features.GetRoleList;

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
        public async Task<int> CreateUser(UserCreate.Command cmd)
        {
            return await _mediator.Send(cmd);
        }

        [HttpPost("getbyname")]
        public async Task<User> GetUserList(Features.GetUserByName.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("list")]
        public async Task<User[]> GetUserList()
        {
            return await _mediator.Send(new Features.GetUsersList.Query());
        }

        [HttpGet("employeelist")]
        public async Task<List<PersonModel>> GetEmployeeList()
        {
            var userlist = await _mediator.Send(new Features.GetUsersList.Query());
            var rolelist = await _mediator.Send(new RoleListQuery());
            var res = new List<PersonModel>();
            foreach(var item in userlist)
            {
                var role = rolelist.First(x => x.Id == item.RoleId) as Role;
                if (role.Name == "admin")
                    continue;
                res.Add(new PersonModel()
                {
                    Id = item.Id,
                    FullName = item.GetFullName(),
                    Role = role.Name
                });
            }
            if (res.Count == 0)
                return null;
            return res;
        }
    }
}
