using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Features.PayStatusCreate;
using static СontractAccountingSystem.Core.Features.UserCreate.UserCreate;

namespace СontractAccountingSystem.Core.Features.UserCreate
{
    public class CommandHandler : IRequestHandler<Command, int>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public CommandHandler(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = request.User;

            await _userManager.CreateAsync(user);
            await _mediator.Publish(new UserCreated(user.Id));
            return user.Id;
        }
    }
}
