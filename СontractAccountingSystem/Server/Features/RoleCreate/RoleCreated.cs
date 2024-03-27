using MediatR;

namespace СontractAccountingSystem.Server.Features.RoleCreate
{
    public class RoleCreated : INotification
    {
        public int Id { get; set; }
        public RoleCreated(int id)
        {
            Id = id;
        }
    }
}
