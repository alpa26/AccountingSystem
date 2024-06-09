using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserByName
{
    public class UserByNameQueryHandler : IRequestHandler<UserByNameQuery, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly Repository _repository;

        public UserByNameQueryHandler(Repository repository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _repository = repository;

        }

        public async Task<User> Handle(UserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Name);
            user.Role = await _repository.FindByIdAsync<Role>(user.RoleId);
            return user;
        }
    }
}
