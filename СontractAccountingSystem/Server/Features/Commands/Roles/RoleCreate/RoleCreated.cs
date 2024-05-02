using MediatR;

namespace СontractAccountingSystem.Server.Commands.Roles.RoleCreate
{
    public class RoleCreated : INotification
    {
        public Guid Id { get; set; }
        public RoleCreated(Guid id)
        {
            Id = id;
        }
    }
}
