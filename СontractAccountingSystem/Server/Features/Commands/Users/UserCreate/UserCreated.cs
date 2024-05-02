using MediatR;

namespace СontractAccountingSystem.Server.Commands.Users.UserCreate
{
    public class UserCreated : INotification
    {
        public Guid Id { get; set; }
        public UserCreated(Guid id)
        {
            Id = id;
        }
    }
}
