using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.PayStatuses.GetPayStatusList
{
    public class PayStatusListQueryHandler : IRequestHandler<PayStatusListQuery, List<DocStatus>>
    {
        private readonly Repository _repository;
        public PayStatusListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocStatus>> Handle(PayStatusListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<DocStatus>();
            return items;
        }
    }
}
