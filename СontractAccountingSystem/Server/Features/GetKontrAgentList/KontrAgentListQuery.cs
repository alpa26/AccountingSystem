using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetKontrAgentList
{
    public class KontrAgentListQuery : IRequest<List<KontrAgent>>
    {
    }
}
