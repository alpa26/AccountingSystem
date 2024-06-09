using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentListByAccess
{
    public class DocumentListByAccessQuery : IRequest<List<ArchiveDocumentModel>>
    {
        public Guid Id { get; set; }
        public DocumentListByAccessQuery(Guid id)
        {
            Id = id;
        }
    }
}
