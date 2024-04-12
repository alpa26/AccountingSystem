using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.Queries.Users.GetWorkerList
{
    public class WorkerListQuery : IRequest<List<PersonModel>>
    {
    
    }
}
