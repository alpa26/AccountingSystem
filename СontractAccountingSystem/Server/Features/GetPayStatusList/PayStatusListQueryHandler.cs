using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetPayStatusList
{
    public class PayStatusListQueryHandler : IRequestHandler<PayStatusListQuery, List<DocPayStatus>>
    {
        private readonly Repository _repository;
        public PayStatusListQueryHandler (Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocPayStatus>> Handle(PayStatusListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<DocPayStatus>();
            return items;
        }
    }
}
