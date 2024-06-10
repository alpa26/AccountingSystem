using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.KontrAgents.ChangeKontrAgent
{
    public class ChangeKontrAgentCommand : IRequest<RequestResult>
    {
        public KontrAgentModel KontrAgentModel { get; set; }
        public ChangeKontrAgentCommand(KontrAgentModel kontrAgent)
        {
            KontrAgentModel = kontrAgent;
        }
    }
}
