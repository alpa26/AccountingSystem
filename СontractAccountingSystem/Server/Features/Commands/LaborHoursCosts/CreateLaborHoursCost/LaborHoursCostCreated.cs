using MediatR;

namespace СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost
{
    public class LaborHoursCostCreated : INotification
    {
        public bool Response { get; set; }
        public LaborHoursCostCreated(bool response)
        {
            Response = response;
        }
    }
}
