using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.KontrAgents.GetKontrAgentList
{
    public class KontrAgentListQueryHandler : IRequestHandler<KontrAgentListQuery, List<KontrAgentModel>>
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;


        public KontrAgentListQueryHandler(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        async Task<List<KontrAgentModel>> IRequestHandler<KontrAgentListQuery, List<KontrAgentModel>>.Handle
            (KontrAgentListQuery request,
            CancellationToken cancellationToken)
        {
            var reslist = new List<KontrAgentModel>();
            var items = await _repository.FindListAsync<KontrAgent>();
            var kaTypes = await _repository.FindListAsync<KontrAgentType>();

            foreach (var item in items)
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
