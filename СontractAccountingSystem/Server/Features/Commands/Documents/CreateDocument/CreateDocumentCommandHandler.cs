using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList;
using СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.CreateDocument
{

    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;

        public CreateDocumentCommandHandler(IMediator mediator, Repository repository)
        {
            _mediator = mediator;
           _repository = repository;
        }

        public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var paymentTypes = await _repository.FindListAsync<DocPayType>();
            var docStatuses = await _repository.FindListAsync<DocStatus>();

            var doctypes = await _repository.FindListAsync<DocType>();
            Document doc = null;
            doc = new Document()
            {
                Id = request.Document.Id,
                Number = request.Document.DocumentNumber,
                Name = request.Document.Name,
                CreatedDate = request.Document.CreateDate,
                DeadlineStart = request.Document.DeadlineStart,
                DeadlineEnd = request.Document.DeadlineEnd,
                Price = request.Document.FullPrice,
                Comment = request.Document.Comment,
                WorkDescription = request.Document.EssenceOfAgreement,
                OrganizationId = request.Document.OrganizationName.Id,
                //WorkerId = request.Document.WorkerName.Id,
                KontrAgentId = request.Document.KontrAgentName.Id,
                TypeId = doctypes.First(x => x.Name == request.Document.DocumentType).Id,
                PaymentTypeId = paymentTypes.First(x => x.Name == request.Document.PaymentType.ToString()).Id,
                DocStatusId = docStatuses.First(x => x.Name == request.Document.Status.ToString()).Id,

            };

            var res = await _repository.CreateAsync(doc);
            if (res != null)
            {
                await _mediator.Publish(new DocumentCreated(request.Document.Id));
                return res.Id;
            }
            else
                return Guid.Empty;
                
        }
    }
}
