using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class WorkedLaborHours : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [NotMapped]
    public Payment Payment { get; set; } = new();
    public Guid PaymenttId { get; set; }

    [NotMapped]
    public Worker Worker { get; set; } = new();
    public Guid WorkerId { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal WorkedHours { get; set; }

    public WorkedLaborHours()
    {

    }
}
