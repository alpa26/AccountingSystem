using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentList
{
    public class DocumentListQueryHandler : IRequestHandler<DocumentListQuery, List<Document>>
    {
        private readonly Repository _repository;
        public DocumentListQueryHandler(Repository repository)
        {
            _repository = repository;
        }

        async Task<List<Document>> IRequestHandler<DocumentListQuery, List<Document>>.Handle(DocumentListQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.FindAsync<Document>();
            return items;
        }
    }
}
