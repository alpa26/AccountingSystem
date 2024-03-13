using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.GetUsersList
{
    public class Query: IRequest<User[]>
    {
    }
}
