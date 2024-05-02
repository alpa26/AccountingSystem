using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Server.Commands.Roles.RoleCreate;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;


        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<Guid> CreateRole(RoleCreateCommand cmd)
        {
            return await _mediator.Send(cmd);
        }
    }
}
