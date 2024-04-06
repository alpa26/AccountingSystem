using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentById
{
    public class DocumentByIdQuery : IRequest<Document>
    {
        public int Id { get; set; }
        public DocumentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
