using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationById
{
    public class OrganizationByIdQuery : IRequest<Organization>
    {
        public Guid Id { get; set; }
        public OrganizationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
