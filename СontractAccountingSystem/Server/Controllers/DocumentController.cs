using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Commands.Documents.ChangeDocument;
using СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument;
using СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost;
using СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.ChangeRelatedDocuments;
using СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.CreateRelatedDocuments;
using СontractAccountingSystem.Server.Features.CreateDocument;
using СontractAccountingSystem.Server.Features.CreatePayment;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentById;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentList;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentListByAccess;

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
            if(request.LaborHoursCost.Length!=0)
            {
                var LaborHoursCostIsCreated = await _mediator.Send(new CreateLaborHoursCostCommand(request.LaborHoursCost.ToList()));
                if (!LaborHoursCostIsCreated)
                    return BadRequest();
            }
            if (request.RelatedDocuments.Length != 0)
            {
                var RelateDocIsCreated = await _mediator.Send(
                    new CreateRelatedDocumentsCommand(
                        new RelateDocumentModel() { RelatedDocumentId = res, DocumentName = request.Name, DocumentNumber =request.DocumentNumber },
                    request.RelatedDocuments.ToList())) ;
                if (!RelateDocIsCreated)
                    return BadRequest();
            }
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> ChangeDocument([FromBody] ArchiveDocumentModel request)
        {
            var res = await _mediator.Send(new ChangeDocumentCommand(request));
            if (!res)
                return BadRequest();
            var RelateDocIsChanged = await _mediator.Send(
                new ChangeRelatedDocumentsCommand(new RelateDocumentModel() { 
                    RelatedDocumentId = request.Id,
                    DocumentName = request.Name,
                    DocumentNumber = request.DocumentNumber }, 
                    request.RelatedDocuments.ToList()));
            if (!RelateDocIsChanged)
                return BadRequest();
            return Ok();
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
            var claimrole = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            if(claimrole.Equals("admin"))
                return await _mediator.Send(new DocumentListQuery());
            else
            {
                string claimid = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                return await _mediator.Send(new DocumentListByAccessQuery(new Guid(claimid)));

            }

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
