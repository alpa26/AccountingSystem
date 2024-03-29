using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
using СontractAccountingSystem.Server.Features.PayStatusCreate;
using СontractAccountingSystem.Server.Services;

namespace СontractAccountingSystem.Server.Features.ChangeDocument
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
            var res = await _repository.ChangeAsync(request.Document);
            return res;
        }
    }
}
