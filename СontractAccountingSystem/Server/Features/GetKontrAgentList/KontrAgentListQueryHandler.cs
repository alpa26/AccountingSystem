using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetKontrAgentList
{
    public class KontrAgentListQueryHandler : IRequestHandler<KontrAgentListQuery, List<KontrAgent>>
    {
        private readonly Repository _repository;
        public KontrAgentListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        async Task<List<KontrAgent>> IRequestHandler<KontrAgentListQuery, List<KontrAgent>>.Handle(KontrAgentListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<KontrAgent>();
            return items;
        }
    }
}
