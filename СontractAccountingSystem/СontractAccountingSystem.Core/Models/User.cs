using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models;

public class User 
{
    public string Name { get; set; } = "null";
    public string SecondName { get; set; } = "null";
    public string LastName { get; set; } = "null";
    public string Phone { get; set; } = "null";
    public string Mail { get; set; } = "null";
    public Guid RoleId1 { get; set; }
    public Role Role { get; set; } = new();

    public User()
    {

    }
}
