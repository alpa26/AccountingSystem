using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Core.Models;
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
        private readonly IMapper _mapper;

        public ChangeDocumentCommandHandler(IMediator mediator, Repository repository, IMapper mapper)
        {
            _mediator = mediator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ChangeDocumentCommand request, CancellationToken cancellationToken)
        {

            var paymentTypes = await _repository.FindListAsync<DocPayType>();
            var doctypes = await _repository.FindListAsync<DocType>();
            var paystatuslist = await _repository.FindListAsync<PaymentStatus>();


            var doc = await _repository.FindByIdAsync<Document>(request.Document.Id);

            var paymentChangedList = new List<Payment>();
            foreach (var item in request.Document.PaymentTerms)
            {
                var payment = _mapper.Map<Payment>(item);
                payment.DocumentId = doc.Id;
                payment.PaymentStatusId = paystatuslist.First(x => x.Name == item.Status.ToString()).Id;
                payment.Document = null;
                payment.PaymentStatus = null;
                paymentChangedList.Add(payment);
            }
            var DBpayments = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", doc.Id);


            if (DBpayments.Count > paymentChangedList.Count)
                for (int i =0; i < DBpayments.Count;i++)
                {
                    var changedPayment = paymentChangedList.FirstOrDefault(x => x.Id == DBpayments[i].Id);
                    if (changedPayment is null)
                        await _repository.RemoveAsync<Payment>(DBpayments[i].Id);
                    else
                    {
                        DBpayments[i].DeadlineStart = changedPayment.DeadlineStart;
                        DBpayments[i].DeadlineStart = changedPayment.DeadlineStart;
                        DBpayments[i].DeadlineEnd = changedPayment.DeadlineEnd;
                        DBpayments[i].PaymentStatusId = changedPayment.PaymentStatusId;
                        DBpayments[i].Amount = changedPayment.Amount;
                        await _repository.ChangeAsync(DBpayments[i]);
                    }
                }
            else      
                for (int i = 0; i < paymentChangedList.Count; i++)
                {
                    var DBitem = DBpayments.FirstOrDefault(x => x.Id == paymentChangedList[i].Id);
                    if (DBitem is null)
                    {
                        var create = await _repository.CreateAsync(paymentChangedList[i]);
                        if (create == null)
                            return false;
                    }
                    else
                    {
                        DBitem.DeadlineStart = paymentChangedList[i].DeadlineStart;
                        DBitem.DeadlineStart = paymentChangedList[i].DeadlineStart;
                        DBitem.DeadlineEnd = paymentChangedList[i].DeadlineEnd;
                        DBitem.PaymentStatusId = paymentChangedList[i].PaymentStatusId;
                        DBitem.Amount = paymentChangedList[i].Amount;
                        await _repository.ChangeAsync(DBitem);
                    }
                        
                }

            doc.Number = request.Document.DocumentNumber;
            doc.Name = request.Document.Name;
            doc.CreatedDate = request.Document.CreateDate;
            doc.DeadlineStart = request.Document.DeadlineStart;
            doc.DeadlineEnd = request.Document.DeadlineEnd;
            doc.Price = request.Document.FullPrice;
            doc.Comment = request.Document.Comment;
            doc.WorkDescription = request.Document.EssenceOfAgreement;
            doc.OrganizationId = request.Document.OrganizationName.Id;
            doc.KontrAgentId = request.Document.KontrAgentName.Id;
            doc.TypeId = doctypes.First(x => x.Name == request.Document.DocumentType).Id;
            doc.PaymentTypeId = paymentTypes.First(x => x.Name == request.Document.PaymentType.ToString()).Id;

            

            var res = await _repository.ChangeAsync(doc);

            return res;
        }
    }
}
