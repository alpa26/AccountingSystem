using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models;

public class User : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = "null";
    public string SecondName { get; set; } = "null";
    public string LastName { get; set; } = "null";
    public string Phone { get; set; } = "null";
    public string Mail { get; set; } = "null";
    public bool IsBlocked { get; set; }=false;
    public int RoleId { get; set; }
    public Role Role { get; set; } = new();

    public User()
    {

    }
}
