using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Users.UserCreate
{
    public class UserCreateCommand : IRequest<Guid>
    {
        public User User { get; set; }
        public UserCreateCommand(User user)
        {
            User = user;
        }
    }
}
