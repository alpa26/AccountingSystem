using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public class DocumentCreateCommand : IRequest<int>
    {
        public Document Document { get; set; }
        public DocumentCreateCommand(Document document)
        {
            Document = document;
        }
    }
}
