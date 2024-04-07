using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentById
{
    public class DocumentByIdQuery : IRequest<ArchiveDocumentModel>
    {
        public int Id { get; set; }
        public DocumentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
