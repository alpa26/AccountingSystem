using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.DocumentCreate;
using СontractAccountingSystem.Server.Features.GetDocTypeList;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Features.GetKontrAgentList;
using СontractAccountingSystem.Server.Features.GetOrganizationList;
using СontractAccountingSystem.Server.Features.GetPaymentTypeList;
using СontractAccountingSystem.Server.Features.GetPayStatusList;
using static СontractAccountingSystem.Server.Controllers.AuthController;

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
        public async Task<IActionResult> CreateRole([FromBody] ArchiveDocumentModel request)
        {
            var doc = new Document()
            {
                Number = request.DocumentNumber,
                Name = request.Name,
                CreatedDate = request.CreateDate,
                DeadlineStart = request.DeadlineStart,
                DeadlineEnd = request.DeadlineEnd,
                Price = request.FullPrice,
                Comment = request.Comment,
                WorkDescription = request.EssenceOfAgreement,
            };
            var res = await _mediator.Send(new DocumentCreate.Command(doc));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("list")]
        public async Task<List<Document>> GetDocumentList()
        {
            return await _mediator.Send(new DocumentListQuery());
        }

        [HttpGet("editlist")]
        public async Task<List<ArchiveDocumentModel>> GetDocumentEditList()
        {
            var reslist = new List<ArchiveDocumentModel>();

            var docList = await _mediator.Send(new DocumentListQuery());

            var orgList = await _mediator.Send(new OrganizationListQuery());
            var userList = await _mediator.Send(new Features.GetUsersList.Query());
            var payStatusList = await _mediator.Send(new PayStatusListQuery());
            var paymentTypeList = await _mediator.Send(new PaymentTypeListQuery());
            var docTypeList = await _mediator.Send(new DocTypeListQuery());
            var kontrAgentList = await _mediator.Send(new KontrAgentListQuery());

            foreach (var item in docList)
            {
                reslist.Add(new ArchiveDocumentModel()
                {
                    Id = new Guid(),
                    DocumentNumber = item.Number,
                    Name = item.Name,
                    DocumentType = docTypeList.FirstOrDefault(x => x.Id == item.TypeId).Name ?? "Дефолт договор",
                    EssenceOfAgreement = item.WorkDescription,
                    //KontrAgentName = item.KontrAgentId.ToString(),
                    KontrAgentName = "КонтрАгент"+ item.KontrAgentId.ToString(),
                    FullPrice = item.Price,
                    EmployerName = userList.FirstOrDefault(x => x.Id == item.EmployerId).FirstName ?? "Васек",
                    Comment = item.Comment,
                    PaymentType = Core.Models.PaymentTypeEnum.FullPostPayment,
                    OrganizationName = orgList.FirstOrDefault(x => x.Id == item.OrganizationId).Name ?? "Шаурмячная",
                    CreateDate = item.CreatedDate,
                    DeadlineStart = item.DeadlineStart,
                    DeadlineEnd = item.DeadlineEnd,
                    RelatedDocuments = new RelateDocumentModel[] { null },
                }) ; 
            }

            return reslist;

        }
    }

}
