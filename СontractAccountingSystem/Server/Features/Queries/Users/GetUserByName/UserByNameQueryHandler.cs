using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserByName
{
    public class UserByNameQueryHandler : IRequestHandler<UserByNameQuery, User>
    {
        private readonly UserManager<User> _userManager;
        public UserByNameQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(UserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Name);
            return user;
        }
    }
}
