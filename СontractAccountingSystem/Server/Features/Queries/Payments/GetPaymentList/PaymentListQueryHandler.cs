using AutoMapper;
using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Queries.Payments.GetPaymentList
{
    public class PaymentListQueryHandler : IRequestHandler<PaymentListQuery, List<PaymentTermModel>>
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;
        private readonly DocumentService _documentService;

        public PaymentListQueryHandler(Repository repository, IMapper mapper, DocumentService documentService)
        {
            _repository = repository;
            _mapper= mapper;
            _documentService = documentService;
        }

        public async Task<List<PaymentTermModel>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
        {
            var docList = await _repository.FindListAsync<Document>();

            var paymentModelList = new List<PaymentTermModel>();
            var DBPayments = await _repository.FindListAsync<Payment>();
            foreach (var entity in DBPayments)
                paymentModelList.Add(_mapper.Map<PaymentTermModel>(entity));

            foreach (var item in paymentModelList)
            {
                var doc = docList.FirstOrDefault(x => x.Number == item.DocumentNumber);
                item.DocumentNumber = doc.Number;
                item.DocumentName = doc.Name;
                var HoursWorkedList = await _documentService.GetLaborHoursModel<WorkedLaborHours, Guid>("PaymentId", item.Id);
                item.LaborHoursWorked = HoursWorkedList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray();
            }

            return paymentModelList;
        }
    }
}
