using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public int Id { get; set; }
        public GetDocumentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
