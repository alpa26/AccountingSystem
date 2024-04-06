using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Users.UserCreate
{
    public class UserCreateCommand : IRequest<int>
    {
        public User User { get; set; }
        public UserCreateCommand(User user)
        {
            User = user;
        }
    }
}
