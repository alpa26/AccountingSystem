using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.Queries.Users.GetEmployeeList
{
    public class EmployeeListQuery : IRequest<List<PersonModel>>
    {
    
    }
}
