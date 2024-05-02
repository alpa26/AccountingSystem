using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.CreateDocument
{
    public class CreateDocumentCommand : IRequest<Guid>
    {
        public ArchiveDocumentModel Document { get; set; }
        public CreateDocumentCommand(ArchiveDocumentModel document)
        {
            Document = document;
        }
    }
}
