using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList
{
    public class PaymentTypeListQueryHandler : IRequestHandler<PaymentTypeListQuery, List<PaymentType>>
    {
        private readonly Repository _repository;
        public PaymentTypeListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<PaymentType>> Handle(PaymentTypeListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<PaymentType>();
            return items;
        }
    }
}
