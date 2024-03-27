using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class DocPayStatus : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DocPayStatus() { }
    public DocPayStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
