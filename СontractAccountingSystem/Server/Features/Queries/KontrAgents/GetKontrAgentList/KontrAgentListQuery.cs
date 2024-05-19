using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList
{
    public class KontrAgentListQuery : IRequest<List<KontrAgentModel>>
    {
    }
}
