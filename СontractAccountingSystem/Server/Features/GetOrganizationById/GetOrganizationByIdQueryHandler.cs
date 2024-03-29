using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentById;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetOrganizationById
{
    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Organization>
    {
        private readonly Repository _repository;
        public GetOrganizationByIdQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<Organization> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repository.FindByIdAsync<Organization>(request.Id);
            return res;
        }
    }
}
