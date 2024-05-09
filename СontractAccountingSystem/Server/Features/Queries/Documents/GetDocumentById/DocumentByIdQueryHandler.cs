using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Salazki.Presentation.Elements;
using System.Collections.Generic;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentList;
using СontractAccountingSystem.Server.Queries.Users.GetUserById;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentById
{
    public class DocumentByIdQueryHandler : IRequestHandler<DocumentByIdQuery, ArchiveDocumentModel>
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;
        private readonly DocumentService _documentService;

        public DocumentByIdQueryHandler(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArchiveDocumentModel> Handle(DocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await _repository.FindByIdAsync<Document>(request.Id);
            doc.Type = await _repository.FindByIdAsync<DocType>(doc.TypeId);
            doc.PaymentType = await _repository.FindByIdAsync<DocPayType>(doc.PaymentTypeId);
            doc.KontrAgent = await _repository.FindByIdAsync<KontrAgent>(doc.KontrAgentId);
            doc.Organization = await _repository.FindByIdAsync<Organization>(doc.OrganizationId);
            
            var paymentModelList = new List<PaymentTermModel>();
            var paymententitiesList = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", doc.Id);
            foreach (var entity in paymententitiesList)
                paymentModelList.Add(_mapper.Map<PaymentTermModel>(entity));


            foreach (var item in paymentModelList)
            {
                item.DocumentNumber = doc.Number;
                item.DocumentName = doc.Name;
                var HoursWorkedList = await GetLaborHoursModel<WorkedLaborHours, Guid>("PaymentId", item.Id);
                item.LaborHoursWorked = HoursWorkedList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray();
            }
            var laborHoursCostList = await GetLaborHoursModel<LaborHoursCost, Guid>("DocumentId", doc.Id);

            //

            var relatedDocmodelist = new List<RelateDocumentModel>();
            var DBrelatedDoclist = await _repository.FindListByFilterAsync<RelateDocuments, Guid>("Document1Id", doc.Id);
            foreach(var item in DBrelatedDoclist)
            {
                var newmodel = _mapper.Map<RelateDocumentModel>(item);
                newmodel.DocumentNumber = item.Document2Number;
                newmodel.DocumentName = item.Document2Name;
                relatedDocmodelist.Add(newmodel);
            }

            var model = new ArchiveDocumentModel()
            {
                Id = doc.Id,
                DocumentNumber = doc.Number,
                Name = doc.Name,
                DocumentType = doc.Type.Name,
                EssenceOfAgreement = doc.WorkDescription,
                KontrAgentName = new KontrAgentModel() { Id = doc.KontrAgent.Id, FullName = doc.KontrAgent.FullName, INN = doc.KontrAgent.INN },
                FullPrice = doc.Price,
                //WorkerName = workers == null ? null : new PersonModel()
                //{
                //    Id = workers.Id,
                //    FullName = workers.GetFullName(),
                //    Role = workers.Position
                //},
                Comment = doc.Comment,
                PaymentType = (PaymentTypeEnum)Enum.Parse(typeof(PaymentTypeEnum), doc.PaymentType.Name),
                OrganizationName = doc.Organization == null ? null : new OrganizationModel() { Id = doc.Organization.Id, Name = doc.Organization.Name },
                CreateDate = doc.CreatedDate,
                DeadlineStart = doc.DeadlineStart,
                DeadlineEnd = doc.DeadlineEnd,
                RelatedDocuments = relatedDocmodelist.ToArray(),
                LaborHoursCost = laborHoursCostList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray(),
                PaymentTerms = paymentModelList.ToArray(),
                
            };

            return model;
        }
        public async Task<List<LaborHoursModel>> GetLaborHoursModel<T1,T2>(string property, T2 value) where T1 : class, IEntity, IWorker
        {
            var workers = await _repository.FindListAsync<Worker>();

            var modellist = new List<LaborHoursModel>();
            var entitiesList = await _repository.FindListByFilterAsync<T1, T2>(property, value);
            foreach (var entity in entitiesList)
            {
                var newitem = _mapper.Map<LaborHoursModel>(entity);
                newitem.WorkerName = _mapper.Map<PersonModel>(workers.FirstOrDefault(x=> x.Id == entity.WorkerId));
                modellist.Add(newitem);
            }
            return modellist;
        }
    }

}
