using AutoMapper;
using MediatR;
using Salazki.Presentation.Elements;
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
            var kontrAgentList = await _repository.FindListAsync<KontrAgent>();
            var orgList = await _repository.FindListAsync<Organization>();

            var statusList = await _repository.FindListAsync<PaymentStatus>();


            var paymentModelList = new List<PaymentTermModel>();

            var DBPayments = await _repository.FindListAsync<Payment>();

            foreach (var entity in DBPayments)
            {
                var newitem = _mapper.Map<PaymentTermModel>(entity);
                var status = statusList.FirstOrDefault(x => x.Id == entity.PaymentStatusId);
                newitem.Status = (PaymentStatusEnum)Enum.Parse(typeof(PaymentStatusEnum), status.Name);
                paymentModelList.Add(newitem);
            }

            foreach (var item in paymentModelList)
            {
                var doc = docList.FirstOrDefault(x => x.Number == item.DocumentNumber);
                var ka = kontrAgentList.FirstOrDefault(x => x.Id == doc.KontrAgentId);
                var org = orgList.FirstOrDefault(x => x.Id == doc.OrganizationId);


                item.DocumentNumber = doc.Number;
                item.DocumentName = doc.Name;
                item.KontrAgentName = new KontrAgentModel() { Id = ka.Id, FullName = ka.FullName, INN = ka.INN };
                item.OrganizationName = doc.Organization == null ? null : new OrganizationModel() { Id = org.Id, Name = org.Name};
                var HoursWorkedList = await _documentService.GetLaborHoursModel<WorkedLaborHours, Guid>("PaymentId", item.Id);
                item.LaborHoursWorked = HoursWorkedList.Select(x => { x.DocumentNumber = doc.Number; return x; }).ToArray();
            }

            return paymentModelList;
        }
    }
}
