using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument
{
    public class DeleteDocumentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteDocumentCommand(int id)
        {
            Id = id;
        }
    }
}
