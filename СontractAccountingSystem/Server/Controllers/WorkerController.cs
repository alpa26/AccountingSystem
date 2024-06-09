using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Features.Commands.Workers.CreateWorker;
using СontractAccountingSystem.Server.Features.Queries.Workers.GetWorkerList;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/workers")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorker([FromBody] PersonModel worker)
        {
            var res = await _mediator.Send(new CreateWorkerCommand(worker));
            if (res.Success)
                return Ok();
            else return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<PersonModel>> GetWorkerList()
        {
            return await _mediator.Send(new WorkerListQuery());
        }
    }
}
