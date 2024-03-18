using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Features.GetUserByName
{
    public class QueryHandler : IRequestHandler<Query, User>
    {
        private readonly UserManager<User> _userManager;
        public QueryHandler(UserManager<User> userManager) {
            _userManager = userManager;
        }

        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Name);
            return user;
        }
    }
}
