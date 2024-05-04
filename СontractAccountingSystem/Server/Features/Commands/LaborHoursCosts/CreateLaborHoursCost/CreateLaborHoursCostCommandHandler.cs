using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.CreatePayment;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost
{
    public class CreateLaborHoursCostCommandHandler : IRequestHandler<CreateLaborHoursCostCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;


        public CreateLaborHoursCostCommandHandler(IMediator mediator, Repository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Handle(CreateLaborHoursCostCommand request, CancellationToken cancellationToken)
        {
            var doclist = await _repository.FindListAsync<Document>();
            var workers = await _repository.FindListAsync<Worker>();

            foreach (var costs in request.LaborHourCosts)
            {
                LaborHoursCost newlaborCost = new LaborHoursCost()
                {
                    Id = costs.Id,
                    Document = null,
                    DocumentId = doclist.First(x => x.Number == costs.DocumentNumber).Id,
                    Worker = null,
                    WorkerId = workers.First(x => x.Id == costs.WorkerName.Id).Id,
                    HourlyRate = costs.HourlyRate,
                };
                var res = await _repository.CreateAsync(newlaborCost);
                if (res == null)
                {
                    return false;
                }
            }
            try {
                await _mediator.Publish(new LaborHoursCostCreated(true));
            }
            catch (Exception ex) { }
            
            return true;
        }
    }
}
