using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetRoleList
{
    public class RoleListQuery : IRequest<List<Role>>
    {
    }
}
