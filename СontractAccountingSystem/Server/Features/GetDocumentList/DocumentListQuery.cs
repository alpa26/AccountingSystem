using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetDocumentList
{
    public class DocumentListQuery : IRequest<List<Document>>
    {

    }
}
