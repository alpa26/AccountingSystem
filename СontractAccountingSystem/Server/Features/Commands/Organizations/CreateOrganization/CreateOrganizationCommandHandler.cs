using AutoMapper;
using MediatR;
using СontractAccountingSystem.Server.Commands.KontrAgents.KontrAgentCreate;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.Organizations.CreateOrganization
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, RequestResult>
    {
        private readonly IMapper _mapper;
        private readonly Repository _repository;

        public CreateOrganizationCommandHandler(IMapper mapper, Repository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RequestResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Organization>(request.OrganizationModel);
            var res = await _repository.CreateAsync(entity);
            if (res is not null)
                return new RequestResult(true);
            else
                return new RequestResult(false);
        }
    }
}
