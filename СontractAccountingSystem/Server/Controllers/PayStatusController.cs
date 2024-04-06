using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate;
using СontractAccountingSystem.Server.Data;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/paystatus")]
    [ApiController]
    public class PayStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly AppDbContext _appDbContext;


        public PayStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<int> CreatePayStatus(PayStatusCreateCommand cmd)
        {
            return await _mediator.Send(cmd);
        }

        /*
        [HttpPost("create1")]
        public async Task<int> CreatePayStatus1(DocPayStatus docPayStatus)
        {
            var result = await _appDbContext.DocPayStatuses.AddAsync(docPayStatus);

            if (result.State != EntityState.Added)
                return -1;
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return -1;
            }

            return docPayStatus.Id;
        }
        */

        /*
        [HttpPost("remove")]
        public async Task RemovePayStatus(PayStatusCreate.Command cmd)
        {
            await _mediator.Send(cmd);
        }
        */

    }
}
