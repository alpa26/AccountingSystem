using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.Roles.GetRoleList;
using СontractAccountingSystem.Server.Queries.Users.GetUsersList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Queries.Users.GetEmployeeList
{
    public class EmployeeListQueryHandler : IRequestHandler<EmployeeListQuery, List<PersonModel>>
    {
        private readonly Repository _repository;

        public EmployeeListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonModel>> Handle(EmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employeelist = await _repository.FindAsync<Employee>();
            var res = new List<PersonModel>();
            foreach (var item in employeelist)
            {
                res.Add(new PersonModel()
                {
                    Id = item.Id,
                    FullName = item.GetFullName(),
                    Role = item.Position
                });
            }
            return res.ToList();
        }
    }
}
