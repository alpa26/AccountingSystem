using MediatR;

namespace СontractAccountingSystem.Server.Commands.PayStatuses.PayStatusCreate
{
    public class PayStatusCreated : INotification
    {
        public int Id { get; set; }
        public PayStatusCreated(int id)
        {
            Id = id;
        }
    }
}
