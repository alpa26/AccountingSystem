using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.ChangeDocument
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

