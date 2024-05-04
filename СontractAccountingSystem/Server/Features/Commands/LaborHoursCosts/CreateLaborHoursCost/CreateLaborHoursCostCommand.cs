using MediatR;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities;

namespace СontractAccountingSystem.Server.Features.Commands.LaborHoursCosts.CreateLaborHoursCost
{
    public class CreateLaborHoursCostCommand : IRequest<bool>
    {
        public List<LaborHoursModel> LaborHourCosts { get; set; }
        public CreateLaborHoursCostCommand(LaborHoursModel cost)
        {
            LaborHourCosts.Add(cost);
        }
        public CreateLaborHoursCostCommand(List<LaborHoursModel> costs)
        {
            LaborHourCosts = costs;
        }
    }
}
