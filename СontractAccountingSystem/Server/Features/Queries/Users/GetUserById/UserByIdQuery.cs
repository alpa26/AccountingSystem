using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserById
{
    public class UserByIdQuery : IRequest<UserModel>
    {
        public Guid Id { get; set; }
        public UserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
