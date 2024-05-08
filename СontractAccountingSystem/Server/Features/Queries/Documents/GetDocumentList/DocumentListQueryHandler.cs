using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;
using СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList;
using СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList;
using СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList;
using СontractAccountingSystem.Server.Queries.Roles.GetRoleList;
using СontractAccountingSystem.Server.Queries.Users.GetUsersList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentList
{
    public class DocumentListQueryHandler : IRequestHandler<DocumentListQuery, List<ArchiveDocumentModel>>
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        public DocumentListQueryHandler(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        async Task<List<ArchiveDocumentModel>> IRequestHandler<DocumentListQuery, List<ArchiveDocumentModel>>.Handle(DocumentListQuery request, CancellationToken cancellationToken)
        {
            var reslist = new List<ArchiveDocumentModel>();

            var docList = await _repository.FindListAsync<Document>();
            var paytypeList = await _repository.FindListAsync<DocPayType>();
            var orgList = await _repository.FindListAsync<Organization>();
            var workerList = await _repository.FindListAsync<Worker>();
            var docTypeList = await _repository.FindListAsync<DocType>();
            var kontrAgentList = await _repository.FindListAsync<KontrAgent>();

            foreach (var doc in docList)
            {
                var ka = kontrAgentList.FirstOrDefault(x => x.Id == doc.KontrAgentId);
                //var workers = workerList.FirstOrDefault(x => x.Id == item.WorkerId);
                var org = orgList.FirstOrDefault(x => x.Id == doc.OrganizationId);

                var paymentModelList = new List<PaymentTermModel>();
                var paymententitiesList = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", doc.Id);
                foreach (var entity in paymententitiesList)
                    paymentModelList.Add(_mapper.Map<PaymentTermModel>(entity));


                foreach (var item in paymentModelList)
                {
                    item.DocumentNumber = doc.Number;
                    var HoursWorkedList = await GetLaborHoursModel<WorkedLaborHours, Guid>("PaymentId", item.Id);
                    item.LaborHoursWorked = HoursWorkedList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray();
                }
                var laborHoursCostList = await GetLaborHoursModel<LaborHoursCost, Guid>("DocumentId", doc.Id);

                reslist.Add(new ArchiveDocumentModel()
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
                    RelatedDocuments = new RelateDocumentModel[] { null },
                    LaborHoursCost = laborHoursCostList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray(),
                    PaymentTerms = paymentModelList.ToArray()
                });
            }

            return reslist;
        }

        public async Task<List<LaborHoursModel>> GetLaborHoursModel<T1, T2>(string property, T2 value) where T1 : class, IEntity, IWorker
        {
            var workers = await _repository.FindListAsync<Worker>();

            var modellist = new List<LaborHoursModel>();
            var entitiesList = await _repository.FindListByFilterAsync<T1, T2>(property, value);
            foreach (var entity in entitiesList)
            {
                var newitem = _mapper.Map<LaborHoursModel>(entity);
                newitem.WorkerName = _mapper.Map<PersonModel>(workers.FirstOrDefault(x => x.Id == entity.WorkerId));
                modellist.Add(newitem);
            }
            return modellist;
        }
    }
}
