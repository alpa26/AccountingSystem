using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteKontrAgent
{
    public class DeleteKontrAgentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteKontrAgentCommand(Guid id)
        {
            Id = id;
        }
    }
}
