using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList;
using СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.DocumentCreate
{

    public class DocumentCreateCommandHandler : IRequestHandler<DocumentCreateCommand, int>
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;

        public DocumentCreateCommandHandler(IMediator mediator, Repository repository)
        {
            _mediator = mediator;
           _repository = repository;
        }

        public async Task<int> Handle(DocumentCreateCommand request, CancellationToken cancellationToken)
        {
            var paymentTypes = await _repository.FindAsync<PaymentType>();
            var doctypes = await _repository.FindAsync<DocType>();
            var doc = new Document()
            {
                Number = request.Document.DocumentNumber,
                Name = request.Document.Name,
                CreatedDate = request.Document.CreateDate,
                DeadlineStart = request.Document.DeadlineStart,
                DeadlineEnd = request.Document.DeadlineEnd,
                Price = request.Document.FullPrice,
                Comment = request.Document.Comment,
                WorkDescription = request.Document.EssenceOfAgreement,
                OrganizationId = request.Document.OrganizationName.Id,
                EmployerId = request.Document.EmployerName.Id,
                KontrAgentId = request.Document.KontrAgentName.Id,
                TypeId = doctypes.First(x => x.Name == request.Document.DocumentType).Id,
                PaymentTypeId = paymentTypes.First(x => x.Name == request.Document.PaymentType.ToString()).Id
            };
            var res = await _repository.CreateAsync(doc);
            if (res != null)
            {
                await _mediator.Publish(new DocumentCreated(request.Document.Id));
                return request.Document.Id;
            }
            else
                return -1;
                
        }
    }
}
