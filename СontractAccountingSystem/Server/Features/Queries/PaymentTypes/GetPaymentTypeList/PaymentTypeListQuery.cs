using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList
{
    public class PaymentTypeListQuery : IRequest<List<DocPayType>>
    {

    }
}
