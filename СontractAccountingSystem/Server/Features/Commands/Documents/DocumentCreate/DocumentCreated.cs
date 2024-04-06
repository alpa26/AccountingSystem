using MediatR;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public class DocumentCreated : INotification
    {
        public int Response { get; set; }
        public DocumentCreated(int response)
        {
            Response = response;
        }
    }
}
