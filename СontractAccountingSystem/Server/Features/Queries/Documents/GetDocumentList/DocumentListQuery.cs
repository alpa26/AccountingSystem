using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.Documents.GetDocumentList
{
    public class DocumentListQuery : IRequest<List<ArchiveDocumentModel>>
    {

    }
}
