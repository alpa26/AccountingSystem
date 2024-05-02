using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList
{
    public class KontrAgentListQueryHandler : IRequestHandler<KontrAgentListQuery, List<KontrAgent>>
    {
        private readonly Repository _repository;
        public KontrAgentListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        async Task<List<KontrAgent>> IRequestHandler<KontrAgentListQuery, List<KontrAgent>>.Handle
            (KontrAgentListQuery request,
            CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<KontrAgent>();
            return items;
        }
    }
}
