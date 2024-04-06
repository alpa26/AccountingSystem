using MediatR;
using System;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate
{
    public class PayStatusCreateCommand: IRequest<int>
    {
        public DocPayStatus DocPayStatus { get; set; }
        public PayStatusCreateCommand(DocPayStatus status) {
            DocPayStatus = status; 
        }
    }
}
