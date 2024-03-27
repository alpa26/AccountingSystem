using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetOrganizationList
{
    public class OrganizationListQueryHandler : IRequestHandler<OrganizationListQuery, List<Organization>>
    {
        private readonly Repository _repository;
        public OrganizationListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<Organization>> Handle(OrganizationListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<Organization>();
            return items;
        }

    }
}
