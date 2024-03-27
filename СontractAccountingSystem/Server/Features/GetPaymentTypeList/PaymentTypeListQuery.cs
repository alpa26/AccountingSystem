using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetPaymentTypeList
{
    public class PaymentTypeListQuery : IRequest<List<PaymentType>>
    {

    }
}
