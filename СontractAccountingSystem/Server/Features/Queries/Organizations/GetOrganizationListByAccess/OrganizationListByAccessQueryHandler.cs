using AutoMapper;
using MediatR;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Organizations.GetOrganizationListByAccess
{
    public class OrganizationListByAccessQueryHandler : IRequestHandler<OrganizationListByAccessQuery, List<OrganizationModel>>
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrganizationListByAccessQueryHandler(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationModel>> Handle(OrganizationListByAccessQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.Id);
            var res = new List<OrganizationModel>();
            foreach (var item in user.Organizations)
                res.Add(_mapper.Map<OrganizationModel>(item));
            return res;
        }

    }
}
