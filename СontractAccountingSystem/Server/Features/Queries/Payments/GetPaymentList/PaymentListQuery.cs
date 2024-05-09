using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Payments.GetPaymentList
{
    public class PaymentListQuery : IRequest<List<PaymentTermModel>>
    {

    }
}
