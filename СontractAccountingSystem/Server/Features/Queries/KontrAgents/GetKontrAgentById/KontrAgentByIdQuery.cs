using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentById
{
    public class KontrAgentByIdQuery : IRequest<KontrAgent>
    {
        public int Id { get; set; }
        public KontrAgentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
