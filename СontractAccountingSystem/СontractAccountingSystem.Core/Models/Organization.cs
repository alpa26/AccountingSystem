using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models;

public class Organization : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "null";
    public Organization()
    {
    }
}
