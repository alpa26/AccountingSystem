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
        public DocumentListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        async Task<List<ArchiveDocumentModel>> IRequestHandler<DocumentListQuery, List<ArchiveDocumentModel>>.Handle(DocumentListQuery request, CancellationToken cancellationToken)
        {
            var reslist = new List<ArchiveDocumentModel>();

            var docList = await _repository.FindAsync<Document>();
            var orgList = await _repository.FindAsync<Organization>();
            var userList = await _repository.FindAsync<User>();
            var roleList = await _repository.FindAsync<Role>();
            var docTypeList = await _repository.FindAsync<DocType>();
            var kontrAgentList = await _repository.FindAsync<KontrAgent>();

            foreach (var item in docList)
            {
                var ka = kontrAgentList.FirstOrDefault(x => x.Id == item.KontrAgentId);
                var empl = userList.FirstOrDefault(x => x.Id == item.EmployerId);
                var org = orgList.FirstOrDefault(x => x.Id == item.OrganizationId);
                reslist.Add(new ArchiveDocumentModel()
                {
                    Id = item.Id,
                    DocumentNumber = item.Number,
                    Name = item.Name,
                    DocumentType = docTypeList.FirstOrDefault(x => x.Id == item.TypeId).Name,
                    EssenceOfAgreement = item.WorkDescription,
                    KontrAgentName = new KontrAgentModel() { Id = ka.Id, FullName = ka.FullName, INN = ka.INN },
                    FullPrice = item.Price,
                    EmployerName = new PersonModel()
                    {
                        Id = empl.Id,
                        FullName = empl.GetFullName(),
                        Role = roleList.FirstOrDefault(x => x.Id == empl.RoleId).Name
                    },
                    Comment = item.Comment,
                    PaymentType = PaymentTypeEnum.FullPostPayment,    /// !!!!!!!!!!!!!!
                    OrganizationName = new OrganizationModel() { Id = org.Id, Name = org.Name },
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
