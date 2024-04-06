using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Documents.ChangeDocument
{
    public class ChangeDocumentCommand : IRequest<bool>
    {
        public Document Document { get; set; }
        public ChangeDocumentCommand(Document document)
        {
            Document = document;
        }
    }
}

