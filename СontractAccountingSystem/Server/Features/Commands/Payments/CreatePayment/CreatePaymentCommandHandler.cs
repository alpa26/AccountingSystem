using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using СontractAccountingSystem.Core.Models;
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
                    Comment = paymentitem.Comment,
                    Amount = paymentitem.Amount
                };
                var createdPayment = await _repository.CreateAsync(payment);
                if (createdPayment == null)
                    return false;
                var WorkedHoursIsCreated = await CreateWorkedHours(paymentitem.LaborHoursWorked, payment.Id);
                if (!WorkedHoursIsCreated)
                    return false;
            }
            await _mediator.Publish(new PaymentCreated(true));
            return true;
        }

        public async Task<bool> CreateWorkedHours(LaborHoursModel[] workedHours,Guid paymentId)
        {
            var workers = await _repository.FindListAsync<Worker>();

            if (workedHours == null)
                return false;
            foreach (var hours in workedHours)
            {
                var newWorkedHours = new WorkedLaborHours()
                {
                    Id = hours.Id,
                    Payment = null,
                    Worker = null,
                    WorkerId = workers.First(x => x.Id == hours.WorkerName.Id).Id,
                    PaymenttId= paymentId,
                    HourlyRate= hours.HourlyRate,
                    WorkedHours = hours.Hours
                };
                var res = await _repository.CreateAsync(newWorkedHours);
                if (res == null)
                    return false;
            }
            return true;
        }
    }
}
