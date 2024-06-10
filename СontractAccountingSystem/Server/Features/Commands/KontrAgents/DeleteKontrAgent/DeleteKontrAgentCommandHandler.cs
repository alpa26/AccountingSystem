using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteKontrAgent
{
    public class DeleteKontrAgentCommandHandler : IRequestHandler<DeleteKontrAgentCommand, bool>
    {
        private readonly Repository _repository;

        public DeleteKontrAgentCommandHandler(IMediator mediator, Repository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteKontrAgentCommand request, CancellationToken cancellationToken)
        {
            var res = await _repository.RemoveAsync<KontrAgent>(request.Id);
            return res;
        }
    }
}
