using MediatR;

namespace СontractAccountingSystem.Core.Features.RoleCreate
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
