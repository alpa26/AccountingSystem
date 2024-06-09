using AutoMapper;
using MediatR;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.Workers.CreateWorker
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand, RequestResult>
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public CreateWorkerCommandHandler(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RequestResult> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var newentity = _mapper.Map<Worker>(request.WorkerModel);
            var arr = request.WorkerModel.FullName.Split(' ');
            newentity.SecondName = arr[0];
            newentity.FirstName = arr[1];
            newentity.LastName = arr[2];

            var res = await _repository.CreateAsync(newentity);
            if (res is not null)
                return new RequestResult(true);
            else
                return new RequestResult(false);
        }
    }
}
