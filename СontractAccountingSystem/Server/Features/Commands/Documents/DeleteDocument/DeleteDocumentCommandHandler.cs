using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.Commands.Documents.DeleteDocument
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
    {
        private readonly Repository _repository;

        public DeleteDocumentCommandHandler(IMediator mediator, Repository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var paymentEntitiesList = await _repository.FindListByFilterAsync<Payment, Guid>("DocumentId", request.Id);
            foreach(var item in paymentEntitiesList)
                    await _repository.RemoveAsync<Payment>(item.Id);
            var res = await _repository.RemoveAsync<Document>(request.Id);
            return res;
        }
    }
}
