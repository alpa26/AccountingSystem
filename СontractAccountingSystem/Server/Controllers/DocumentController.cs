using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Salazki.Presentation.Elements;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.ChangeDocument;
using СontractAccountingSystem.Server.Features.DocumentCreate;
using СontractAccountingSystem.Server.Features.GetDocTypeList;
using СontractAccountingSystem.Server.Features.GetDocumentById;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Features.GetKontrAgentById;
using СontractAccountingSystem.Server.Features.GetKontrAgentList;
using СontractAccountingSystem.Server.Features.GetOrganizationById;
using СontractAccountingSystem.Server.Features.GetOrganizationList;
using СontractAccountingSystem.Server.Features.GetPaymentTypeList;
using СontractAccountingSystem.Server.Features.GetPayStatusList;
using СontractAccountingSystem.Server.Features.GetRoleList;
using СontractAccountingSystem.Server.Features.GetUserById;
using СontractAccountingSystem.Server.Services.Interfaces;
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
        public async Task<IActionResult> CreateDocument([FromBody] ArchiveDocumentModel request)
        {
            var paystatuses = await _mediator.Send(new PaymentTypeListQuery());
            var doctypes = await _mediator.Send(new DocTypeListQuery());

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
                OrganizationId = request.OrganizationName.Id,
                EmployerId = request.EmployerName.Id,
                KontrAgentId = request.KontrAgentName.Id,
                TypeId = doctypes.First(x => x.Name == request.DocumentType).Id,
                PayStatusId= paystatuses.First(x=>x.Name == request.PaymentType.ToString()).Id
            };
            var res = await _mediator.Send(new DocumentCreate.Command(doc));
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> ChangeDocument([FromBody] ArchiveDocumentModel request)
        {
            var paystatuses = await _mediator.Send(new PaymentTypeListQuery());
            var doctypes = await _mediator.Send(new DocTypeListQuery());
            var doc = await _mediator.Send(new GetDocumentByIdQuery(request.Id));

            doc.Number = request.DocumentNumber;
            doc.Name = request.Name;
            doc.CreatedDate = request.CreateDate;
            doc.DeadlineStart = request.DeadlineStart;
            doc.DeadlineEnd = request.DeadlineEnd;
            doc.Price = request.FullPrice;
            doc.Comment = request.Comment;
            doc.WorkDescription = request.EssenceOfAgreement;
            doc.OrganizationId = request.OrganizationName.Id;
            doc.EmployerId = request.EmployerName.Id;
            doc.KontrAgentId = request.KontrAgentName.Id;
            doc.TypeId = doctypes.First(x => x.Name == request.DocumentType).Id;
            doc.PayStatusId = paystatuses.First(x => x.Name == request.PaymentType.ToString()).Id;
            var res = await _mediator.Send(new ChangeDocumentCommand(doc));
            if (res)
                return Ok();
            else
                return BadRequest();
        }


        [HttpGet("getbyid")]
        public async Task<Document> GetDocumentById(int id)
        {
            var res = await _mediator.Send(new GetDocumentByIdQuery(id));
            return res;
        }

        [HttpGet("getmodelbyid")]
        public async Task<ArchiveDocumentModel> GetDocumentModelById(int id)
        {
            var doc = await _mediator.Send(new GetDocumentByIdQuery(id));
            var empl = await _mediator.Send(new GetUserByIdQuery(doc.EmployerId));

            var model = new ArchiveDocumentModel()
            {
                Id = doc.Id,
                DocumentNumber = doc.Number,
                Name = doc.Name,
                DocumentType = doc.PaymentType.Name,
                EssenceOfAgreement = doc.WorkDescription,
                KontrAgentName = new KontrAgentModel() { Id = doc.KontrAgent.Id, FullName = doc.KontrAgent.FullName, INN = doc.KontrAgent.INN },
                FullPrice = doc.Price,
                EmployerName = new PersonModel()
                {
                    Id = empl.Id,
                    FullName = empl.GetFullName(),
                    Role = empl.Role.Name
                },
                Comment = doc.Comment,
                PaymentType = (PaymentTypeEnum)Enum.Parse(typeof(PaymentTypeEnum), doc.PaymentType.Name),
            OrganizationName = new OrganizationModel() { Id = doc.Organization.Id, Name = doc.Organization.Name },
                CreateDate = doc.CreatedDate,
                DeadlineStart = doc.DeadlineStart,
                DeadlineEnd = doc.DeadlineEnd,
                RelatedDocuments = new RelateDocumentModel[] { null }
            };

            return model;
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
            var roleList = await _mediator.Send(new RoleListQuery());
            var docTypeList = await _mediator.Send(new DocTypeListQuery());
            var kontrAgentList = await _mediator.Send(new KontrAgentListQuery());

            foreach (var item in docList)
            {
                var ka = kontrAgentList.FirstOrDefault(x => x.Id == item.KontrAgentId);
                var empl = userList.FirstOrDefault(x => x.Id == item.EmployerId);
                var org = orgList.FirstOrDefault(x => x.Id == item.OrganizationId);
                reslist.Add(new ArchiveDocumentModel()
                {
                    Id =item.Id,
                    DocumentNumber = item.Number,
                    Name = item.Name,
                    DocumentType = docTypeList.FirstOrDefault(x => x.Id == item.TypeId).Name,
                    EssenceOfAgreement = item.WorkDescription,
                    KontrAgentName = new KontrAgentModel() { Id= ka.Id, FullName = ka.FullName, INN = ka.INN},
                    FullPrice = item.Price,
                    EmployerName = new PersonModel() { Id = empl.Id,FullName = empl.GetFullName(),
                        Role = roleList.FirstOrDefault(x => x.Id == empl.RoleId).Name},
                    Comment = item.Comment,
                    PaymentType = PaymentTypeEnum.FullPostPayment,    /// !!!!!!!!!!!!!!
                    OrganizationName = new OrganizationModel() { Id = org.Id, Name = org.Name},
                    CreateDate = item.CreatedDate,
                    DeadlineStart = item.DeadlineStart,
                    DeadlineEnd = item.DeadlineEnd,
                    RelatedDocuments = new RelateDocumentModel[] { null }
                }); 
            }

            return reslist;

        }
    }

}
