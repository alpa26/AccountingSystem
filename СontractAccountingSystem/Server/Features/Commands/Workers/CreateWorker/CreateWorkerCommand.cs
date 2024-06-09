using MediatR;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Server.Features.Commands.Workers.CreateWorker
{
    public class CreateWorkerCommand : IRequest<RequestResult>
    {
        public PersonModel WorkerModel { get; set; }
        public CreateWorkerCommand(PersonModel worker)
        {
            WorkerModel = worker;
        }
    }
}
