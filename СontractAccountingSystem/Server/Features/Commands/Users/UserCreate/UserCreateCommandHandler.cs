using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Users.UserCreate
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public UserCreateCommandHandler(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<int> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;

            await _userManager.CreateAsync(user);
            await _mediator.Publish(new UserCreated(user.Id));
            return user.Id;
        }
    }
}
