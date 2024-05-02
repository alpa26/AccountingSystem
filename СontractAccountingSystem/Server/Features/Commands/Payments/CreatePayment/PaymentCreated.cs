using MediatR;

namespace СontractAccountingSystem.Server.Features.CreatePayment
{
    public class PaymentCreated : INotification
    {
        public bool Response { get; set; }
        public PaymentCreated(bool response)
        {
            Response = response;
        }
    }
}
