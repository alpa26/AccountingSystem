using MediatR;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.CreatePayment;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.CreateRelatedDocuments
{
    public class CreateRelatedDocumentsCommandHandler : IRequestHandler<CreateRelatedDocumentsCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly DocumentService _documentService;


        public CreateRelatedDocumentsCommandHandler(IMediator mediator, DocumentService documentService)
        {
            _mediator = mediator;
            _documentService = documentService;
        }

        public async Task<bool> Handle(CreateRelatedDocumentsCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.RelatedDocuments)
            {
                bool relatedDocumentCreated = await _documentService.CreateRelateDocumentAsync( 
                                                                    request.Parent, 
                                                                    item);
                if(!relatedDocumentCreated)
                    return false;
            }
            return true;
        }
    }
}
