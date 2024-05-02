using MediatR;

namespace СontractAccountingSystem.Server.Features.CreateDocument
{
    public class DocumentCreated : INotification
    {
        public Guid Response { get; set; }
        public DocumentCreated(Guid response)
        {
            Response = response;
        }
    }
}
