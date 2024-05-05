using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class LaborHoursCost : IEntity, IWorker
{
    [Key]
    public Guid Id { get; set; }
    [NotMapped]
    public Document Document { get; set; } = new();
    public Guid DocumentId { get; set; }

    [NotMapped]
    public Worker Worker { get; set; } = new();
    public Guid WorkerId { get; set; }

    public decimal HourlyRate { get; set; }

    public LaborHoursCost()
    {

    }
}
