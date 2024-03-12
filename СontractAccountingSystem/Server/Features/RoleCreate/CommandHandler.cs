using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Features.PayStatusCreate;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.RoleCreate
{
    public partial class RoleCreate
    {
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly IMediator _mediator;
            private readonly RoleManager<Role> _roleManager;

            public CommandHandler(IMediator mediator, RoleManager<Role> roleManager)
            {
                _mediator = mediator;
                _roleManager = roleManager;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var role = request.Role;

                await _roleManager.CreateAsync(role);
                await _mediator.Publish(new PayStatusCreated(role.Id));
                return role.Id;
            }
        }


    }
}
