using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentById
{
    public class KontrAgentByIdQuery : IRequest<KontrAgent>
    {
        public Guid Id { get; set; }
        public KontrAgentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
