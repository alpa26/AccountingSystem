using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetUsersList
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
