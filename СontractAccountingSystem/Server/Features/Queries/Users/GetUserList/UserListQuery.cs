using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUsersList
{
    public class UserListQuery : IRequest<User[]>
    {
    }
}
