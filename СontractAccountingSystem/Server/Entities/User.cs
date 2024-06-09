using Microsoft.AspNetCore.Identity;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    [Key]
    public override Guid Id { get; set; }
    public override string UserName { get; set; } = "null";
    public string FirstName { get; set; } = "null";
    public string SecondName { get; set; } = "null";
    public string LastName { get; set; } = "null";
    public override string PhoneNumber { get; set; } = "null";
    public override string Email { get; set; } = "null";
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = new();

    public List<Document> Documents { get; set; } = new List<Document>();
    public List<KontrAgent> KontrAgents { get; set; } = new List<KontrAgent>();
    public List<Organization> Organizations { get; set; } = new List<Organization>();

    public User()
    {

    }

    public string GetFullName()
    {
        return String.Format("{0} {1}.{2}.", SecondName, FirstName[0], LastName[0]);
    }
}
