using MediatR;

namespace СontractAccountingSystem.Core.Features.PayStatusCreate
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
