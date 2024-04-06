using MediatR;

namespace СontractAccountingSystem.Server.Commands.Roles.RoleCreate
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
