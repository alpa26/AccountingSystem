using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.GetDocTypeList
{
    public class DocTypeListQuery : IRequest<List<DocType>>
    {

    }
}
