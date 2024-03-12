using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models.Interfaces;

namespace СontractAccountingSystem.Core.Models;

public class User : IdentityUser<int>
{
    [Key]
    public override int Id { get; set; }
    public override string UserName { get; set; } = "null";
    public string FirstName { get; set; } = "null";
    public string SecondName { get; set; } = "null";
    public string LastName { get; set; } = "null";
    public override string PhoneNumber { get; set; } = "null";
    public override string Email { get; set; } = "null";
    public int RoleId { get; set; }
    public Role Role { get; set; } = new();

    public User()
    {

    }
}
