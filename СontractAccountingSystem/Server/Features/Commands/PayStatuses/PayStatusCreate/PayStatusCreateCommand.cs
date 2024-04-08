using MediatR;
using System;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate
{
    public class PayStatusCreateCommand: IRequest<int>
    {
        public DocStatus DocPayStatus { get; set; }
        public PayStatusCreateCommand(DocStatus status) {
            DocPayStatus = status; 
        }
    }
}
