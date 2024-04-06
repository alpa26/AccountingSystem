using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationById
{
    public class OrganizationByIdQuery : IRequest<Organization>
    {
        public int Id { get; set; }
        public OrganizationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
