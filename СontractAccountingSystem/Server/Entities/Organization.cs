using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class Organization : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = "null";
    public List<User> Users { get; } = new List<User>();

    public Organization()
    {
    }
}
