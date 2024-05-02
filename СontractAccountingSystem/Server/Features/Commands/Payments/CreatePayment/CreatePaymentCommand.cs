using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.CreatePayment
{
    public class CreatePaymentCommand : IRequest<bool>
    {
        public List<PaymentTermModel> Payments { get; set; }
        public CreatePaymentCommand(PaymentTermModel payment)
        {
            Payments.Add(payment);
        }
        public CreatePaymentCommand(List<PaymentTermModel> payments)
        {
            Payments = payments;
        }
    }
}
