using MediatR;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public partial class DocumentCreate
    {
        public class DocumentCreated : INotification
        {
            public bool Response { get; set; }
            public DocumentCreated(bool response)
            {
                Response = response;
            }
        }
    }
}
