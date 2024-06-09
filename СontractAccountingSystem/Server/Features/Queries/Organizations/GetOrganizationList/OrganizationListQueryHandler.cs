using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationList
{
    public class OrganizationListQueryHandler : IRequestHandler<OrganizationListQuery, List<OrganizationModel>>
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        public OrganizationListQueryHandler(Repository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationModel>> Handle(OrganizationListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<Organization>();
            var res = new List<OrganizationModel>();
            foreach (var item in items)
                res.Add(_mapper.Map<OrganizationModel>(item));
            return res;
        }

    }
}
