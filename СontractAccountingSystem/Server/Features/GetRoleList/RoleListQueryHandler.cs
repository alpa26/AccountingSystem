using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetRoleList
{
    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, List<Role>>
    {
        private readonly Repository _repository;
        public RoleListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<Role>> Handle(RoleListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<Role>();
            return items.ToList();
        }
    }
}
