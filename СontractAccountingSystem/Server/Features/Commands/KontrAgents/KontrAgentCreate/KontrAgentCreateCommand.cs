using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features;

namespace СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate
{
    public class KontrAgentCreateCommand : IRequest<RequestResult>
    {
        public KontrAgentModel KontrAgentModel { get; set; }
        public KontrAgentCreateCommand(KontrAgentModel kontrAgent)
        {
            KontrAgentModel = kontrAgent;
        }
    }
}
