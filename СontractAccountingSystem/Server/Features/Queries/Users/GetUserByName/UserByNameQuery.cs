using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Users.GetUserByName
{

    public class UserByNameQuery : IRequest<User>
    {
        public string Name { get; set; }
        public UserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
