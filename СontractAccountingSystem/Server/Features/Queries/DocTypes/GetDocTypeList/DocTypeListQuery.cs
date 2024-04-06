using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList
{
    public class DocTypeListQuery : IRequest<List<DocType>>
    {

    }
}
