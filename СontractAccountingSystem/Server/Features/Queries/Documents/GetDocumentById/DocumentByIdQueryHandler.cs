using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.Users.GetUserById;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentById
{
    public class DocumentByIdQueryHandler : IRequestHandler<DocumentByIdQuery, ArchiveDocumentModel>
    {
        private readonly Repository _repository;

        public DocumentByIdQueryHandler(Repository repository, UserManager<User> userManager)
        {
            _repository = repository;
        }

        public async Task<ArchiveDocumentModel> Handle(DocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await _repository.FindByIdAsync<Document>(request.Id);
            doc.Type = await _repository.FindByIdAsync<DocType>(doc.TypeId);
            doc.PaymentType = await _repository.FindByIdAsync<DocPayType>(doc.PaymentTypeId);
            doc.KontrAgent = await _repository.FindByIdAsync<KontrAgent>(doc.KontrAgentId);
            doc.Organization = await _repository.FindByIdAsync<Organization>(doc.OrganizationId);

            //var workers = await _repository.FindByIdAsync<Worker>(doc.WorkerId);

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
                OrganizationName = doc.Organization == null? null :  new OrganizationModel() { Id = doc.Organization.Id, Name = doc.Organization.Name },
                CreateDate = doc.CreatedDate,
                DeadlineStart = doc.DeadlineStart,
                DeadlineEnd = doc.DeadlineEnd,
                RelatedDocuments = new RelateDocumentModel[] { null }
            };

            return model;
        }
    }
}
