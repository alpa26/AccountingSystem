﻿using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList
{
    public class OrganizationListQueryHandler : IRequestHandler<OrganizationListQuery, List<Organization>>
    {
        private readonly Repository _repository;
        public OrganizationListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<Organization>> Handle(OrganizationListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<Organization>();
            return items;
        }

    }
}
