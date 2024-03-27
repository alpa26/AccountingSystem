using MediatR;

namespace СontractAccountingSystem.Server.Features.UserCreate
{
    public partial class UserCreate
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
}
