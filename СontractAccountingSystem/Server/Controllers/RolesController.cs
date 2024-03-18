using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Features.RoleCreate;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;


        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<int> CreateRole(RoleCreate.Command cmd)
        {
            return await _mediator.Send(cmd);
        }
    }
}
