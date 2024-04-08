using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Queries.DocTypes.GetDocTypeList;
using СontractAccountingSystem.Server.Queries.Documents.GetDocumentById;
using СontractAccountingSystem.Server.Queries.PaymentTypes.GetPaymentTypeList;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Commands.Documents.ChangeDocument
{
    public class ChangeDocumentCommandHandler : IRequestHandler<ChangeDocumentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly Repository _repository;

        public ChangeDocumentCommandHandler(IMediator mediator, Repository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<bool> Handle(ChangeDocumentCommand request, CancellationToken cancellationToken)
        {

            var paymentTypes = await _repository.FindAsync<DocPayType>();
            var doctypes = await _repository.FindAsync<DocType>();
            var doc = await _repository.FindByIdAsync<Document>(request.Document.Id);

            doc.Number = request.Document.DocumentNumber;
            doc.Name = request.Document.Name;
            doc.CreatedDate = request.Document.CreateDate;
            doc.DeadlineStart = request.Document.DeadlineStart;
            doc.DeadlineEnd = request.Document.DeadlineEnd;
            doc.Price = request.Document.FullPrice;
            doc.Comment = request.Document.Comment;
            doc.WorkDescription = request.Document.EssenceOfAgreement;
            doc.OrganizationId = request.Document.OrganizationName.Id;
            doc.EmployerId = request.Document.EmployerName.Id;
            doc.KontrAgentId = request.Document.KontrAgentName.Id;
            doc.TypeId = doctypes.First(x => x.Name == request.Document.DocumentType).Id;
            doc.PaymentTypeId = paymentTypes.First(x => x.Name == request.Document.PaymentType.ToString()).Id;

            var res = await _repository.ChangeAsync(doc);

            return res;
        }
    }
}
