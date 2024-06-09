using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationListByAccess
{
    public class OrganizationListByAccessQuery : IRequest<List<OrganizationModel>>
    {
        public Guid Id { get; set; }
        public OrganizationListByAccessQuery(Guid id)
        {
            Id = id;
        }
    }
}
