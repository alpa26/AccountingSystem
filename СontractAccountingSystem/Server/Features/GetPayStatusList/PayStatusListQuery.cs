using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetPayStatusList
{
    public class PayStatusListQuery : IRequest<List<DocPayStatus>>
    {
    }
}
