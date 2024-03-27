using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class RelateDocuments : IEntity
{
    [Key]
    public int Id { get; set; }
    public int Document1Id { get; set; }
    public Document Document1 { get; set; } = new();
    public int Document2Id { get; set; }
    public Document Document2 { get; set; } = new();
    public RelateDocuments()
    {

    }
}
