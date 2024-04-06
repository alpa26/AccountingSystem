using MediatR;

namespace СontractAccountingSystem.Server.Commands.Documents.ChangeDocument
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
