using MediatR;
using СontractAccountingSystem.Server.Features.PayStatusCreate;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.RoleCreate
{
    public partial class RoleCreate
    {
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly IMediator _mediator;
            private readonly Repository _repository;

            public CommandHandler(IMediator mediator, Repository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var role = request.Role;

                await _repository.CreateAsync(role);
                await _mediator.Publish(new PayStatusCreated(role.Id));
                return role.Id;
            }
        }


    }
}
