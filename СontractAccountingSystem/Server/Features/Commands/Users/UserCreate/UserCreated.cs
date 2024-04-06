using MediatR;

namespace СontractAccountingSystem.Server.Commands.Users.UserCreate
{
    public class UserCreated : INotification
    {
        public int Id { get; set; }
        public UserCreated(int id)
        {
            Id = id;
        }
    }
}
