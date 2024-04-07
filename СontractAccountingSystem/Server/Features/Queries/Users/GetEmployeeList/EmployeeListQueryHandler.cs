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
            var userlist = await _repository.FindAsync<User>();
            var rolelist = await _repository.FindAsync<Role>();
            var res = new List<PersonModel>();
            foreach (var item in userlist)
            {
                var role = rolelist.First(x => x.Id == item.RoleId) as Role;
                if (role.Name == "admin")
                    continue;
                res.Add(new PersonModel()
                {
                    Id = item.Id,
                    FullName = item.GetFullName(),
                    Role = role.Name
                });
            }
            return res.ToList();
        }
    }
}
