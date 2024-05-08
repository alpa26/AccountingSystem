using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.CreateRelatedDocuments
{
    public class CreateRelatedDocumentsCommand : IRequest<bool>
    {
        public RelateDocumentModel Parent { get; set; }
        public List<RelateDocumentModel> RelatedDocuments { get; set; }
        public CreateRelatedDocumentsCommand(RelateDocumentModel parent, RelateDocumentModel relatedDocument)
        {
            Parent = parent;
            RelatedDocuments.Add(relatedDocument);
        }
        public CreateRelatedDocumentsCommand(RelateDocumentModel parent, List<RelateDocumentModel> relatedDocuments)
        {
            Parent = parent;
            RelatedDocuments = relatedDocuments;
        }
    }
}
