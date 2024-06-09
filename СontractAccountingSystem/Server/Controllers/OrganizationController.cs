using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;
using СontractAccountingSystem.Server.Features.Commands.Organizations.CreateOrganization;
using СontractAccountingSystem.Server.Features.Commands.Workers.CreateWorker;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentListByAccess;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationById;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationListByAccess;

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateKontrAgent([FromBody] OrganizationModel model)
        {
            var res = await _mediator.Send(new CreateOrganizationCommand(model));
            if (res.Success)
                return Ok();
            else return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<OrganizationModel>> GetOrganizationList()
        {
            var claimrole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            if (claimrole.Equals("admin"))
                return await _mediator.Send(new OrganizationListQuery());
            else
            {
                string claimid = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return await _mediator.Send(new OrganizationListByAccessQuery(new Guid(claimid)));
            }
        }

        [HttpGet("getbyid")]
        public async Task<Organization> GetOrganizationById(Guid id)
        {
            var res = await _mediator.Send(new OrganizationByIdQuery(id));
            return res;
        }
    }
}
