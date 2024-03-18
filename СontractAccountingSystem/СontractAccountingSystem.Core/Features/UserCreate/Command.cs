using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Features.UserCreate
{
    public partial class UserCreate
    {
        public class Command : IRequest<int>
        {
            public User User { get; set; }
            public Command(User user)
            {
                User = user;
            }
        }
    }
}
