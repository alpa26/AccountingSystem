using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class Role : IdentityRole<Guid>, IEntity
{
    [Key]
    public override Guid Id { get; set; }
    public override string Name { get; set; } = "null";
    public Role()
    {

    }
}
