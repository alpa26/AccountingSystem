using MediatR;

namespace СontractAccountingSystem.Server.Features.Commands.RelatedDocuments.ChangeRelatedDocuments
{
    public class RelatedDocumentsChanged: INotification
    {
        public Guid Id { get; set; }
        public RelatedDocumentsChanged(Guid id)
        {
            Id = id;
        }
    }
}
