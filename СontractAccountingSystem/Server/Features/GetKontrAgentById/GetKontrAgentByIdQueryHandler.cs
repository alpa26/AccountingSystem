using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentById;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetKontrAgentById
{
    public class GetKontrAgentByIdQueryHandler : IRequestHandler<GetKontrAgentByIdQuery, KontrAgent>
    {
        private readonly Repository _repository;
        public GetKontrAgentByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<KontrAgent> Handle(GetKontrAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.FindByIdAsync<KontrAgent>(request.Id);
            return res;
        }
    }
}
