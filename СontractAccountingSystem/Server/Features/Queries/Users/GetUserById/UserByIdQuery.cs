using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserById
{
    public class UserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
        public UserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
