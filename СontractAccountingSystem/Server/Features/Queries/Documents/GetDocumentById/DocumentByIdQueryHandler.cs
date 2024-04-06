using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentById
{
    public class DocumentByIdQueryHandler : IRequestHandler<DocumentByIdQuery, Document>
    {
        private readonly Repository _repository;
        public DocumentByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<Document> Handle(DocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await _repository.FindByIdAsync<Document>(request.Id);
            doc.PaymentType = await _repository.FindByIdAsync<PaymentType>(doc.PaymentTypeId);
            doc.KontrAgent = await _repository.FindByIdAsync<KontrAgent>(doc.KontrAgentId);
            doc.Organization = await _repository.FindByIdAsync<Organization>(doc.OrganizationId);
            return doc;
        }
    }
}
