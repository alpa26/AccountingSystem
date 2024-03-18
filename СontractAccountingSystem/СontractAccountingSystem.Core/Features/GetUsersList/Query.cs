using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Features.GetUsersList
{
    public class Query: IRequest<User[]>
    {
    }
}
