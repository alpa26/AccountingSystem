using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Users.GetUsersList
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, User[]>
    {
        private readonly Repository _repository;

        public UserListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<User[]> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<User>();
            return items.ToArray();
        }
    }
}
