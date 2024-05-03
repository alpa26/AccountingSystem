using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
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
                var payments = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", doc.Id);
                foreach (var item in payments)
                {
                    var newitem = _mapper.Map<PaymentTermModel>(item);
                    newitem.DocumentNumber = doc.Number;
                    paymentModelList.Add(newitem);
                }

                reslist.Add(new ArchiveDocumentModel()
                {
                    Id = doc.Id,
                    DocumentNumber = doc.Number,
                    Name = doc.Name,
                    DocumentType = docTypeList.FirstOrDefault(x => x.Id == doc.TypeId).Name,
                    EssenceOfAgreement = doc.WorkDescription,
                    KontrAgentName = new KontrAgentModel() { Id = ka.Id, FullName = ka.FullName, INN = ka.INN },
                    FullPrice = doc.Price,
                    //WorkerName = workers == null? null: new PersonModel()
                    //{
                    //    Id = workers.Id,
                    //    FullName = workers.GetFullName(),
                    //    Role = workers.Position
                    //},
                    Comment = doc.Comment,
                    PaymentType = (PaymentTypeEnum)Enum.Parse(typeof(PaymentTypeEnum), paytypeList.FirstOrDefault(x => x.Id == doc.PaymentTypeId).Name),
                    OrganizationName = org == null ? null : new OrganizationModel() { Id = org.Id, Name = org.Name },
                    CreateDate = doc.CreatedDate,
                    DeadlineStart = doc.DeadlineStart,
                    DeadlineEnd = doc.DeadlineEnd,
                    RelatedDocuments = new RelateDocumentModel[] { null },
                    PaymentTerms = paymentModelList.ToArray()
                });
            }

            return reslist;
        }
    }
}
