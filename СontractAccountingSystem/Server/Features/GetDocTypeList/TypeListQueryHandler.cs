using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.GetDocumentList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.GetDocTypeList
{
    public class TypeListQueryHandler : IRequestHandler<DocTypeListQuery, List<DocType>>
    {
        private readonly Repository _repository;
        public TypeListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocType>> Handle(DocTypeListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<DocType>();
            return items;
        }
    }
}
