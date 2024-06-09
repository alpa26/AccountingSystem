using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Users.ChangeUser
{
    public class ChangeUserCommand : IRequest<RequestResult>
    {
        public UserModel User { get; set; }
        public ChangeUserCommand(UserModel user)
        {
            User = user;
        }
    }
}
