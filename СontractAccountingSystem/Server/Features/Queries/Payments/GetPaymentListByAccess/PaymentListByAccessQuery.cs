using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Payments.GetPaymentListByAccess
{
    public class PaymentListByAccessQuery : IRequest<List<PaymentTermModel>>
    {
        public Guid Id { get; set; }
        public PaymentListByAccessQuery(Guid id)
        {
            Id = id;
        }
    }
}
