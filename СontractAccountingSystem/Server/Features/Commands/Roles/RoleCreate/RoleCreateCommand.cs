﻿using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.Roles.RoleCreate
{
    public class RoleCreateCommand : IRequest<Guid>
    {
        public Role Role { get; set; }
        public RoleCreateCommand(Role role)
        {
            Role = role;
        }
    }
}
