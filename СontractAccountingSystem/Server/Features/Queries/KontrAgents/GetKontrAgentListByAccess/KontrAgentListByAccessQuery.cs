using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentListByAccess
{
    public class KontrAgentListByAccessQuery : IRequest<List<KontrAgentModel>>
    {
        public Guid Id { get; set; }
        public KontrAgentListByAccessQuery(Guid id)
        {
            Id = id;
        }
    }
}
