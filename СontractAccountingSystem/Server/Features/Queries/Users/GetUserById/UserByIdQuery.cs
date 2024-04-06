using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserById
{
    public class UserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
        public UserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
