using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost;
using СontractAccountingSystem.Server.Features.CreatePayment;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationListByAccess;
using СontractAccountingSystem.Server.Queries.Payments.GetPaymentList;
using СontractAccountingSystem.Server.Queries.Payments.GetPaymentListByAccess;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentAgent([FromBody] PaymentTermModel request)
        {
            var res = await _mediator.Send(new CreatePaymentCommand(request));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("createlist")]
        public async Task<IActionResult> CreatePaymentsAgent([FromBody] List<PaymentTermModel> request)
        {
            var res = await _mediator.Send(new CreatePaymentCommand(request));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<PaymentTermModel>> GetPaymentList()
        {
            var claimrole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            if (claimrole.Equals("admin"))
                return await _mediator.Send(new PaymentListQuery());
            else
            {
                string claimid = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return await _mediator.Send(new PaymentListByAccessQuery(new Guid(claimid)));
            }
        }
    }
}
