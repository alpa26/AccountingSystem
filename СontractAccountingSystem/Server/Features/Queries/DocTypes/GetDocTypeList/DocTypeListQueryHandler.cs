using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList
{
    public class DocTypeListQueryHandler : IRequestHandler<DocTypeListQuery, List<DocType>>
    {
        private readonly Repository _repository;
        public DocTypeListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocType>> Handle(DocTypeListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindListAsync<DocType>();
            return items;
        }
    }
}
