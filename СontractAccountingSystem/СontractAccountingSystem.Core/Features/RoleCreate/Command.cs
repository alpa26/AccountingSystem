using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Features.RoleCreate
{
    public partial class RoleCreate
    {
        public class Command : IRequest<int>
        {
            public Role Role { get; set; }
            public Command(Role role)
            {
                Role = role;
            }
        }
    }
}
