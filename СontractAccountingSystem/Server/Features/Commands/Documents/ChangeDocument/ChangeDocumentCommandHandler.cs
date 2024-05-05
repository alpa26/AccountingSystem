using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Salazki.Presentation.Elements;
using System.Linq.Expressions;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Entities.Interfaces;
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


            var doc = await _repository.FindByIdAsync<Document>(request.Document.Id);


            

            var paymentIsChanged = await ChangePayment(request.Document.PaymentTerms, doc.Id);
            if(!paymentIsChanged)
                return false;
            if(request.Document.DocumentType == "Договор на фактические услуги")
            {
                var HoursCostIsChanged = await ChangeLaborHoursCost(request.Document.LaborHoursCost, doc.Id);
                if (!HoursCostIsChanged)
                    return false;

                foreach (var item in request.Document.PaymentTerms)
                {
                    var WorkedHoursIsChanged = await ChangeWorkedHours(item.LaborHoursWorked, item.Id);
                    if (!WorkedHoursIsChanged)
                        return false;
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

        public async Task<bool> ChangeLaborHoursCost(LaborHoursModel[] laborHoursModel, Guid docId)
        {
            var workers = await _repository.FindListAsync<Worker>();
            var laborChangedList = new List<LaborHoursCost>();
            foreach (var item in laborHoursModel){
                var payment = _mapper.Map<LaborHoursCost>(item);
                payment.WorkerId = workers.First(x => x.Id == item.WorkerName.Id).Id;
                payment.DocumentId = docId;
                payment.Document = null;
                payment.Worker = null;
                laborChangedList.Add(payment);
            }
            var DBLaborCosts = await _repository.FindListByFilterAsync<LaborHoursCost, Guid>("DocumentId", docId);

            for (int i = 0; i < laborChangedList.Count; i++)
            {
                var DBitem = DBLaborCosts.FirstOrDefault(x => x.Id == laborChangedList[i].Id);
                if (DBitem is null){
                    var create = await _repository.CreateAsync(laborChangedList[i]);
                    if (create == null)
                        return false;
                }
                else{
                    DBitem.DocumentId = laborChangedList[i].DocumentId;
                    DBitem.HourlyRate = laborChangedList[i].HourlyRate;
                    DBitem.WorkerId = laborChangedList[i].WorkerId;
                    await _repository.ChangeAsync(DBitem);
                }
            }
            for (int i = 0; i < DBLaborCosts.Count; i++)
            {
                var changedLaborCost = laborChangedList.FirstOrDefault(x => x.Id == DBLaborCosts[i].Id);
                if (changedLaborCost is null)
                    await _repository.RemoveAsync<LaborHoursCost>(DBLaborCosts[i].Id);
            }
            return true;
        }

        public async Task<bool> ChangeWorkedHours(LaborHoursModel[] laborHoursModel, Guid paymentId)
        {
            var workers = await _repository.FindListAsync<Worker>();
            var laborChangedList = new List<WorkedLaborHours>();
            foreach (var item in laborHoursModel){
                var payment = _mapper.Map<WorkedLaborHours>(item);
                payment.WorkerId = workers.First(x => x.Id == item.WorkerName.Id).Id;
                payment.PaymentId = paymentId;
                payment.Payment = null;
                payment.Worker = null;
                laborChangedList.Add(payment);
            }
            var DBWorkedHours = await _repository.FindListByFilterAsync<WorkedLaborHours, Guid>("PaymentId", paymentId);

            for (int i = 0; i < laborChangedList.Count; i++){
                var DBitem = DBWorkedHours.FirstOrDefault(x => x.Id == laborChangedList[i].Id);
                if (DBitem is null){
                    var create = await _repository.CreateAsync(laborChangedList[i]);
                    if (create == null)
                        return false;
                }
                else{
                    DBitem.PaymentId = laborChangedList[i].PaymentId;
                    DBitem.HourlyRate = laborChangedList[i].HourlyRate;
                    DBitem.WorkerId = laborChangedList[i].WorkerId;
                    DBitem.FullAmount = laborChangedList[i].FullAmount;
                    DBitem.Hours = laborChangedList[i].Hours;

                    await _repository.ChangeAsync(DBitem);
                }
            }
            for (int i = 0; i < DBWorkedHours.Count; i++){
                var changedLaborCost = laborChangedList.FirstOrDefault(x => x.Id == DBWorkedHours[i].Id);
                if (changedLaborCost is null)
                    await _repository.RemoveAsync<WorkedLaborHours>(DBWorkedHours[i].Id);
            }
            return true;
        }

        public async Task<bool> ChangePayment(PaymentTermModel[] paymentTermModel, Guid docId)
        {
            var paystatuslist = await _repository.FindListAsync<PaymentStatus>();

            var paymentChangedList = new List<Payment>();
            foreach (var item in paymentTermModel){
                var payment = _mapper.Map<Payment>(item);
                payment.DocumentId = docId;
                payment.PaymentStatusId = paystatuslist.First(x => x.Name == item.Status.ToString()).Id;
                payment.Document = null;
                payment.PaymentStatus = null;
                paymentChangedList.Add(payment);
            }
            var DBpayments = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", docId);
            for (int i = 0; i < paymentChangedList.Count; i++){
                var DBitem = DBpayments.FirstOrDefault(x => x.Id == paymentChangedList[i].Id);
                if (DBitem is null){
                    var create = await _repository.CreateAsync(paymentChangedList[i]);
                    if (create == null)
                        return false;
                }
                else{
                    DBitem.DeadlineStart = paymentChangedList[i].DeadlineStart;
                    DBitem.DeadlineStart = paymentChangedList[i].DeadlineStart;
                    DBitem.DeadlineEnd = paymentChangedList[i].DeadlineEnd;
                    DBitem.PaymentStatusId = paymentChangedList[i].PaymentStatusId;
                    DBitem.Amount = paymentChangedList[i].Amount;
                    await _repository.ChangeAsync(DBitem);
                }
            }
            for (int i = 0; i < DBpayments.Count; i++)
            {
                var changedPayment = paymentChangedList.FirstOrDefault(x => x.Id == DBpayments[i].Id);
                if (changedPayment is null)
                    await _repository.RemoveAsync<Payment>(DBpayments[i].Id);
            }

            return true;
        }
    }
}
