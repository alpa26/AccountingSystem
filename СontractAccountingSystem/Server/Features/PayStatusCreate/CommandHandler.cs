using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.PayStatusCreate
{
    public partial class PayStatusCreate
    {
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly IMediator _mediator;
            private readonly Repository _repository;

            public CommandHandler(IMediator mediator, Repository repository) { 
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var payStatus = request.DocPayStatus;

                await _repository.CreateAsync(payStatus);
                await _mediator.Publish(new PayStatusCreated(payStatus.Id));
                return payStatus.Id;
            }
        }
    }
    
}
