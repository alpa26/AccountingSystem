using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.PayStatuses.GetPayStatusList
{
    public class PayStatusListQuery : IRequest<List<DocStatus>>
    {
    }
}
