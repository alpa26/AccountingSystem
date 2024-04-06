using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Roles.GetRoleList
{
    public class RoleListQuery : IRequest<List<Role>>
    {
    }
}
