using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetUsersList
{
    public class Query: IRequest<User[]>
    {
    }
}
