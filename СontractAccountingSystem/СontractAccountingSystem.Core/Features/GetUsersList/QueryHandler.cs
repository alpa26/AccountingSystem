using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services;

namespace СontractAccountingSystem.Core.Features.GetUsersList
{
    public class QueryHandler : IRequestHandler<Query, User[]>
    {
        private readonly Repository _repository;

        public QueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<User[]> Handle(Query request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<User>();
            return items.ToArray();
        }
    }
}
