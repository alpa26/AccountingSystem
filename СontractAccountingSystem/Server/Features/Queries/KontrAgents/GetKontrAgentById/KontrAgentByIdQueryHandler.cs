using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentById
{
    public class KontrAgentByIdQueryHandler : IRequestHandler<KontrAgentByIdQuery, KontrAgent>
    {
        private readonly Repository _repository;
        public KontrAgentByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<KontrAgent> Handle(KontrAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.FindByIdAsync<KontrAgent>(request.Id);
            return res;
        }
    }
}
