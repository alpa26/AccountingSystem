﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.Documents.ChangeDocument;
using СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument;
using СontractAccountingSystem.Server.Features.CreateDocument;
using СontractAccountingSystem.Server.Features.CreatePayment;
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
            var res = await _mediator.Send(new CreateDocumentCommand(request));
            if (res == Guid.Empty)
                return BadRequest();
            var PaymentIsCreated = await _mediator.Send(new CreatePaymentCommand(request.PaymentTerms.ToList()));
            if (!PaymentIsCreated)
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
        public async Task<ArchiveDocumentModel> GetDocumentById(Guid id)
        {
            var res = await _mediator.Send(new DocumentByIdQuery(id));
            return res;
        }

        [HttpGet("getmodelbyid")]
        public async Task<ArchiveDocumentModel> GetDocumentModelById(Guid id)
        {            
            return await _mediator.Send(new DocumentByIdQuery(id));
        }

        //[HttpGet("list")]
        //public async Task<List<Document>> GetDocumentList()
        //{
        //    return await _mediator.Send(new DocumentListQuery());
        //}

        [HttpGet("geteditlist")]
        public async Task<List<ArchiveDocumentModel>> GetDocumentEditList()
        {
            return await _mediator.Send(new DocumentListQuery());
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var res = await _mediator.Send(new DeleteDocumentCommand(id));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

    }

}
