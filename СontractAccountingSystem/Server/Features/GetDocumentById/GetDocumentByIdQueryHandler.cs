using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Features.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        private readonly Repository _repository;
        public GetDocumentByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<Document> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await _repository.FindByIdAsync<Document>(request.Id);
            doc.PaymentType = await _repository.FindByIdAsync<PaymentType>(doc.PaymentTypeId);
            doc.KontrAgent = await _repository.FindByIdAsync<KontrAgent>(doc.KontrAgentId);
            doc.Organization = await _repository.FindByIdAsync<Organization>(doc.OrganizationId);
            return doc;
        }
    }
}
