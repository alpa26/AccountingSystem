using MediatR;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.CreatePayment;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.ChangeRelatedDocuments
{
    public class ChangeRelatedDocumentsCommandHandler : IRequestHandler<ChangeRelatedDocumentsCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly DocumentService _documentService;
        private readonly Repository _repository;


        public ChangeRelatedDocumentsCommandHandler(IMediator mediator,Repository repository, DocumentService documentService)
        {
            _mediator = mediator;
            _repository = repository;
            _documentService = documentService;
        }

        public async Task<bool> Handle(ChangeRelatedDocumentsCommand request, CancellationToken cancellationToken)
        {
            var DBrelatedDocuments = await _documentService.GetAllRelateDocumentListById(request.Parent.RelatedDocumentId);
            foreach (var item in DBrelatedDocuments)
            {
                if (item.Document2Id == request.Parent.RelatedDocumentId)
                    continue;
                var changedRelatedDocuments = request.RelatedDocuments.FirstOrDefault(x => x.Id == item.Id);
                var ReverseChangedRelatedDocuments = DBrelatedDocuments.FirstOrDefault(x => x.Document1Id == item.Document2Id &&
                                                                                       x.Document2Id == request.Parent.RelatedDocumentId);
                if (changedRelatedDocuments is null)
                {
                    await _repository.RemoveAsync<RelateDocuments>(item.Id);
                    if(ReverseChangedRelatedDocuments is not null)
                        await _repository.RemoveAsync<RelateDocuments>(ReverseChangedRelatedDocuments.Id);
                }
            }
            foreach (var item in request.RelatedDocuments)
            {
                var DBitem = DBrelatedDocuments.FirstOrDefault(x => x.Id == item.Id);
                if (DBitem is null)
                {
                    bool relatedDocumentCreated =
                    await _documentService.CreateRelateDocumentAsync(request.Parent, item);
                    if (!relatedDocumentCreated)
                        return false;
                }
            }
            //for (int i = 0; i < DBrelatedDocuments.Count; i++)
            //{
            //    if (DBrelatedDocuments[i].Document2Id == request.ParentId)
            //        continue;
            //    var changedRelatedDocuments = request.RelatedDocuments.FirstOrDefault(x => x.RelatedDocumentId == DBrelatedDocuments[i].Document2Id);
            //    var ReverseChangedRelatedDocuments = DBrelatedDocuments.FirstOrDefault(x => x.Document1Id == DBrelatedDocuments[i].Document2Id &&
            //                                                                           x.Document2Id == request.ParentId);
            //    if (changedRelatedDocuments is null)
            //    {
            //        await _repository.RemoveAsync<LaborHoursCost>(DBrelatedDocuments[i].Id);
            //        await _repository.RemoveAsync<LaborHoursCost>(ReverseChangedRelatedDocuments.Id);
            //    }
            //}
            
            return true;
        }
    }
}
