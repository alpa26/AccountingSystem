using MediatR;

namespace СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate
{
    public class PayStatusCreated : INotification
    {
        public Guid Id { get; set; }
        public PayStatusCreated(Guid id)
        {
            Id = id;
        }
    }
}
