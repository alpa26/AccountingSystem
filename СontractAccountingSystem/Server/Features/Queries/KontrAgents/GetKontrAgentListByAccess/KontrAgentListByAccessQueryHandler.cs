using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;
using СontractAccountingSystem.Server.Services.Interfaces;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentListByAccess
{
    public class KontrAgentListByAccessQueryHandler : IRequestHandler<KontrAgentListByAccessQuery, List<KontrAgentModel>>
    {
        private readonly Repository _repository;
        private readonly UserRepository _userRepository;

        private readonly IMapper _mapper;


        public KontrAgentListByAccessQueryHandler(Repository repository,UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _repository = repository;
            _mapper = mapper;
        }

        async Task<List<KontrAgentModel>> IRequestHandler<KontrAgentListByAccessQuery, List<KontrAgentModel>>.Handle
            (KontrAgentListByAccessQuery request,
            CancellationToken cancellationToken)
        {
            var reslist = new List<KontrAgentModel>();
            var user = await _userRepository.FindByIdAsync(request.Id);
            var kaTypes = await _repository.FindListAsync<KontrAgentType>();

            foreach (var item in user.KontrAgents)
            {
                var model = _mapper.Map<KontrAgentModel>(item);
                model.ContactPersonName = item.ContactPerson;
                var type = kaTypes.FirstOrDefault(x => x.Id == item.TypeId);
                model.Type = type.Name;
                reslist.Add(model);
            }
            return reslist;
        }
    }
}
