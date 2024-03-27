using MediatR;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{
    public partial class DocumentCreate
    {
        public class Command : IRequest<bool>
        {
            public Document Document { get; set; }
            public Command(Document document)
            {
                Document = document;
            }
        }
    }
}
