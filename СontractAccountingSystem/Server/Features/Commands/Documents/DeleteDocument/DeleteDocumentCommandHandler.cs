using MediatR;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.DocumentCreate;
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

        public Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            return _repository.RemoveAsync<Document>(request.Id);
        }
    }
}
