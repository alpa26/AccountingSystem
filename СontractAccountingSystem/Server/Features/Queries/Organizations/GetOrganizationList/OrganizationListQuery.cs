using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList
{
    public class OrganizationListQuery : IRequest<List<OrganizationModel>>
    {

    }
}
