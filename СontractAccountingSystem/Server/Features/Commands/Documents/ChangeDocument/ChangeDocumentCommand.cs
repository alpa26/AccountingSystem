using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Documents.ChangeDocument
{
    public class ChangeDocumentCommand : IRequest<bool>
    {
        public ArchiveDocumentModel Document { get; set; }
        public ChangeDocumentCommand(ArchiveDocumentModel document)
        {
            Document = document;
        }
    }
}

