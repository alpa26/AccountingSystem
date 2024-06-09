using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentList;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentListByAccess;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentById;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentListByAccess;


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
        public async Task<IActionResult> CreateKontrAgent([FromBody] KontrAgentModel request)
        {
            var res = await _mediator.Send(new KontrAgentCreateCommand(request));
            if (res.Success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<KontrAgentModel>> GetKontrAgentList()
        {
            var claimrole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            if (claimrole.Equals("admin") || claimrole.Equals("director"))
                return await _mediator.Send(new KontrAgentListQuery());
            else
            {
                string claimid = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return await _mediator.Send(new KontrAgentListByAccessQuery(new Guid(claimid)));

            }
        }

        [HttpGet("getbyid")]
        public async Task<KontrAgent> GetKontrAgentById(Guid id)
        {
            return await _mediator.Send(new KontrAgentByIdQuery(id));
        }
    }
}
