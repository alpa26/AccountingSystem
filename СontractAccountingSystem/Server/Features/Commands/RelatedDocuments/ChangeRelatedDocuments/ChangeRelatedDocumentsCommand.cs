using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.ChangeRelatedDocuments
{
    public class ChangeRelatedDocumentsCommand : IRequest<bool>
    {
        public RelateDocumentModel Parent { get; set; }
        public List<RelateDocumentModel> RelatedDocuments { get; set; }
        public ChangeRelatedDocumentsCommand(RelateDocumentModel parent, RelateDocumentModel relatedDocument)
        {
            Parent = parent;
            RelatedDocuments.Add(relatedDocument);
        }
        public ChangeRelatedDocumentsCommand(RelateDocumentModel parent, List<RelateDocumentModel> relatedDocuments)
        {
            Parent = parent;
            RelatedDocuments = relatedDocuments;
        }
    }
}
