using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
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
        public async Task<List<Organization>> GetOrganizationList()
        {
            var res = await _mediator.Send(new OrganizationListQuery());
            return res;
        }
    }
}
