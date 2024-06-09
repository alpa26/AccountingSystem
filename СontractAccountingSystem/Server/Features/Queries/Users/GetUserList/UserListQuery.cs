using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUsersList
{
    public class UserListQuery : IRequest<List<UserModel>>
    {
    }
}
