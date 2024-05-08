using MediatR;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.CreateRelatedDocuments
{
    public class RelatedDocumentsCreated: INotification
    {
        public Guid Id { get; set; }
        public RelatedDocumentsCreated(Guid id)
        {
            Id = id;
        }
    }
}
