using MediatR;
using System;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate
{
    public class PayStatusCreateCommand: IRequest<Guid>
    {
        public DocStatus DocPayStatus { get; set; }
        public PayStatusCreateCommand(DocStatus status) {
            DocPayStatus = status; 
        }
    }
}
