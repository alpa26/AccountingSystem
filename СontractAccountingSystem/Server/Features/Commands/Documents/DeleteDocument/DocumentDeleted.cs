using MediatR;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument
{
    public class DocumentDeleted : INotification
    {
        public bool IsDeleted { get; set; }
        public DocumentDeleted(bool isdeleted)
        {
            IsDeleted = isdeleted;
        }
    }
}
