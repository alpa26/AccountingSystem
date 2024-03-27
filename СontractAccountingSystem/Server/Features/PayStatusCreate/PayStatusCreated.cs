using MediatR;

namespace СontractAccountingSystem.Server.Features.PayStatusCreate
{
    public class PayStatusCreated: INotification
    {
        public int Id { get; set; }
        public PayStatusCreated(int id)
        {
            Id = id;
        }
    }
}
