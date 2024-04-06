using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Roles.RoleCreate
{
        public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, int>
        {
            private readonly IMediator _mediator;
            private readonly RoleManager<Role> _roleManager;

            public RoleCreateCommandHandler(IMediator mediator, RoleManager<Role> roleManager)
            {
                _mediator = mediator;
                _roleManager = roleManager;
            }
            public async Task<int> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
            {
                var role = request.Role;
                await _roleManager.CreateAsync(role);
                await _mediator.Publish(new RoleCreated(role.Id));
                return role.Id;
            }
        }

}
