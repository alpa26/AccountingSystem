using MediatR;
using Microsoft.AspNetCore.Identity;
using СontractAccountingSystem.Server.Entities;
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

            var res = await _repository.CreateAsync(request.Document);
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
