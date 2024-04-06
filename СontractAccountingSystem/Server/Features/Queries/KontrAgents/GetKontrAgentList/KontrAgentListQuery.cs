using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList
{
    public class KontrAgentListQuery : IRequest<List<KontrAgent>>
    {
    }
}
