﻿using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Roles.GetRoleList
{
    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, List<Role>>
    {
        private readonly Repository _repository;
        public RoleListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<Role>> Handle(RoleListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<Role>();
            return items.ToList();
        }
    }
}
