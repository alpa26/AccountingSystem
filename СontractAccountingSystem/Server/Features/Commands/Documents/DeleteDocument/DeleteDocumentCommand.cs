using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteDocumentCommand(Guid id)
        {
            Id = id;
        }
    }
}
