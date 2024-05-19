using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentById;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList;


namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/kontragent")]
    [ApiController]
    public class KontrAgentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KontrAgentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKontrAgent([FromBody] KontrAgent request)
        {
            var res = await _mediator.Send(new KontrAgentCreateCommand(request));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<KontrAgentModel>> GetKontrAgentList()
        {
            return await _mediator.Send(new KontrAgentListQuery());
        }

        [HttpGet("getbyid")]
        public async Task<KontrAgent> GetKontrAgentById(Guid id)
        {
            return await _mediator.Send(new KontrAgentByIdQuery(id));
        }
    }
}
