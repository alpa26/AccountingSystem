using MediatR;

namespace СontractAccountingSystem.Server.Features.ChangeDocument
{
    public class DocumentChanged : INotification
    {
        public bool Flag { get; set; }
        public DocumentChanged(bool flag)
        {
            Flag = flag;
        }
    }
}
