using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationById;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/laborhourscost")]
    [ApiController]
    public class LaborHoursCostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LaborHoursCostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateKontrAgent([FromBody] LaborHoursModel request)
        {
            var res = await _mediator.Send(new CreateLaborHoursCostCommand(request));
            if (res)
                return Ok();
            else
                return BadRequest();
        }
    }
}
