using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.GetUserByName
{

    public class Query : IRequest<User>
    {
        public string Name { get; set; }
        public Query(string name)
        {
            Name = name;
        }
    }
}
