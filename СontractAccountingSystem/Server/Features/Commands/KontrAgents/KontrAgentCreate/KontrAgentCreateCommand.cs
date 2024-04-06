using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate
{
    public class KontrAgentCreateCommand : IRequest<bool>
    {
        public KontrAgent KontrAgent { get; set; }
        public KontrAgentCreateCommand(KontrAgent kontrAgent)
        {
            KontrAgent = kontrAgent;
        }
    }
}
