using MediatR;
using System;
using СontractAccountingSystem.Core.Models;


namespace СontractAccountingSystem.Server.Features.PayStatusCreate
{
    public partial class PayStatusCreate
    {
        public class Command: IRequest<int>
        {
            public DocPayStatus DocPayStatus { get; set; }
            public Command(DocPayStatus status) {
                DocPayStatus = status; 
            }
        }
    }
}
