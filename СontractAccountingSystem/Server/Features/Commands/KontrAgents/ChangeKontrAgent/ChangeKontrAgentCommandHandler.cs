using MediatR;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.KontrAgents.ChangeKontrAgent
{
    public class ChangeKontrAgentCommandHandler : IRequestHandler<ChangeKontrAgentCommand, RequestResult>
    {
        private readonly Repository _repository;

        public ChangeKontrAgentCommandHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult> Handle(ChangeKontrAgentCommand request, CancellationToken cancellationToken)
        {
            var kontragent = await _repository.FindByIdAsync<KontrAgent>(request.KontrAgentModel.Id);

            kontragent.Id = request.KontrAgentModel.Id;
            kontragent.FullName = request.KontrAgentModel.FullName;
            kontragent.INN = request.KontrAgentModel.INN;
            kontragent.KPP = request.KontrAgentModel.KPP;
            kontragent.OGRN = request.KontrAgentModel.OGRN;
            kontragent.ContactPerson = request.KontrAgentModel.ContactPersonName;
            kontragent.ContactPhone = request.KontrAgentModel.ContactPhone;
            kontragent.ContactEmail = request.KontrAgentModel.ContactEmail;
            kontragent.Address = request.KontrAgentModel.Address;


            var types = await _repository.FindListAsync<KontrAgentType>();
            kontragent.TypeId = types.FirstOrDefault(x=>x.Name==request.KontrAgentModel.Type).Id;

            var res = await _repository.ChangeAsync(kontragent);

            if (res)
                return new RequestResult(true);
            else 
                return new RequestResult(false);
        }
    }
}
