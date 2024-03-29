using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetOrganizationById
{
    public class GetOrganizationByIdQuery : IRequest<Organization>
    {
        public int Id { get; set; }
        public GetOrganizationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
