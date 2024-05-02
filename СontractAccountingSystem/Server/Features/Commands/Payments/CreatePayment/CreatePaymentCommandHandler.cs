using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList;
using СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.CreatePayment
{

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;


        public CreatePaymentCommandHandler(IMediator mediator, Repository repository)
        {
            _mediator = mediator;
            _repository = repository;

        }

        public async Task<bool> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var doclist = await _repository.FindListAsync<Document>();
            var paystatuslist = await _repository.FindListAsync<PaymentStatus>();

            foreach (var paymentitem in request.Payments)
            {
                Payment payment = new Payment()
                {
                    Id = paymentitem.Id,
                    DocumentId = doclist.First(x => x.Number == paymentitem.DocumentNumber).Id,
                    DeadlineStart = paymentitem.DeadlineStart,
                    DeadlineEnd = paymentitem.DeadlineEnd,
                    PaymentStatusId = paystatuslist.First(x => x.Name == paymentitem.Status.ToString()).Id,
                    Amount = paymentitem.Amount
                };
                var res = await _repository.CreateAsync(payment);
                if (res == null)
                {
                    return false;
                }
            }
            await _mediator.Publish(new PaymentCreated(true));
            return true;
        }
    }
}
