using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class RelateDocuments : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid Document1Id { get; set; }
    [NotMapped]
    public Document Document1 { get; set; } = new();
    public Guid Document2Id { get; set; }
    [NotMapped]
    public Document Document2 { get; set; } = new();

    public string Document2Name { get; set; }
    public string Document2Number { get; set; }

    public RelateDocuments()
    {

    }
}
