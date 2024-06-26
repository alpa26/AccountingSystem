﻿using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.Roles.GetRoleList;
using СontractAccountingSystem.Server.Queries.Users.GetUsersList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Queries.Workers.GetWorkerList
{
    public class WorkerListQueryHandler : IRequestHandler<WorkerListQuery, List<PersonModel>>
    {
        private readonly Repository _repository;

        public WorkerListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonModel>> Handle(WorkerListQuery request, CancellationToken cancellationToken)
        {
            var employeelist = await _repository.FindListAsync<Worker>();
            var res = new List<PersonModel>();
            foreach (var item in employeelist)
            {
                res.Add(new PersonModel()
                {
                    Id = item.Id,
                    FullName = item.GetFullName(),
                    StaffPosition = item.StaffPosition
                });
            }
            return res.ToList();
        }
    }
}
