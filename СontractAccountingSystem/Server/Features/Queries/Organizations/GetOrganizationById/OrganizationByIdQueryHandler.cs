using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationById
{
    public class OrganizationByIdQueryHandler : IRequestHandler<OrganizationByIdQuery, Organization>
    {
        private readonly Repository _repository;
        public OrganizationByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Handle(OrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.FindByIdAsync<Organization>(request.Id);
            return res;
        }
    }
}
