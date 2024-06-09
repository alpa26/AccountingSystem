using MediatR;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.CreateDocument;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.KontrAgents.KontrAgentCreate
{
    public class KontrAgentCreateCommandHandler : IRequestHandler<KontrAgentCreateCommand, RequestResult>
    {
        private readonly Repository _repository;

        public KontrAgentCreateCommandHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult> Handle(KontrAgentCreateCommand request, CancellationToken cancellationToken)
        {
            var newKontrAgent = new KontrAgent(request.KontrAgentModel);
            var types = await _repository.FindListAsync<KontrAgentType>();
            newKontrAgent.TypeId = types.FirstOrDefault(x=>x.Name==request.KontrAgentModel.Type).Id;
            var res = await _repository.CreateAsync(newKontrAgent);
            if (res is not null)
                return new RequestResult(true);
            else 
                return new RequestResult(false);
        }
    }
}
