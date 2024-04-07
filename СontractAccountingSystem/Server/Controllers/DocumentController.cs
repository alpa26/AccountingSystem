using MediatR;
using Microsoft.AspNetCore.Mvc;

using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.Documents.ChangeDocument;
using СontractAccountingSystem.Server.Features.DocumentCreate;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentById;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentList;

namespace СontractAccountingSystem.Server.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDocument([FromBody] ArchiveDocumentModel request)
        {
            var res = await _mediator.Send(new DocumentCreateCommand(request));
            if (res == -1)
                return BadRequest();
            else
                return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> ChangeDocument([FromBody] ArchiveDocumentModel request)
        {
            var res = await _mediator.Send(new ChangeDocumentCommand(request));
            if (res)
                return Ok();
            else
                return BadRequest();
        }


        [HttpGet("getbyid")]
        public async Task<ArchiveDocumentModel> GetDocumentById(int id)
        {
            var res = await _mediator.Send(new DocumentByIdQuery(id));
            return res;
        }

        [HttpGet("getmodelbyid")]
        public async Task<ArchiveDocumentModel> GetDocumentModelById(int id)
        {            
            return await _mediator.Send(new DocumentByIdQuery(id));
        }

        //[HttpGet("list")]
        //public async Task<List<Document>> GetDocumentList()
        //{
        //    return await _mediator.Send(new DocumentListQuery());
        //}

        [HttpGet("editlist")]
        public async Task<List<ArchiveDocumentModel>> GetDocumentEditList()
        {
            return await _mediator.Send(new DocumentListQuery());

        }
    }

}
