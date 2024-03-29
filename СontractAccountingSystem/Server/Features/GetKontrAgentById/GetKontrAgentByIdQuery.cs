using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetKontrAgentById
{
    public class GetKontrAgentByIdQuery : IRequest<KontrAgent>
    {
        public int Id { get; set; }
        public GetKontrAgentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
