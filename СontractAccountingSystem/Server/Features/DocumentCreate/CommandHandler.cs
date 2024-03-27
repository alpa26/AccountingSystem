using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.PayStatusCreate;
using СontractAccountingSystem.Server.Services;
using static СontractAccountingSystem.Server.Features.DocumentCreate.DocumentCreate;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public partial class DocumentCreate
    {
        public class CommandHandler : IRequestHandler<Command, bool>
        {
            private readonly IMediator _mediator;
            private readonly Repository _repository;

            public CommandHandler(IMediator mediator, Repository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                var res = await _repository.CreateAsync(request.Document);
                if (res != null)
                {
                    await _mediator.Publish(new PayStatusCreated(request.Document.Id));
                    return true;
                }
                else
                    return false;
                
            }
        }
    }
}
