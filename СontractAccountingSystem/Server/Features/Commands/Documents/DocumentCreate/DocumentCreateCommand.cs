using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public class DocumentCreateCommand : IRequest<int>
    {
        public ArchiveDocumentModel Document { get; set; }
        public DocumentCreateCommand(ArchiveDocumentModel document)
        {
            Document = document;
        }
    }
}
