using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Features.GetKontrAgentList;

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

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateKontrAgent([FromBody] KontrAgent request)
        //{
        //    var res = await _mediator.Send();
        //    if (res)
        //        return Ok();
        //    else
        //        return BadRequest();
        //}

        [HttpGet("list")]
        public async Task<List<KontrAgent>> GetKontrAgentList()
        {
            return await _mediator.Send(new KontrAgentListQuery());
        }
    }
}
