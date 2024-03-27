using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetOrganizationList
{
    public class OrganizationListQuery : IRequest<List<Organization>>
    {

    }
}
