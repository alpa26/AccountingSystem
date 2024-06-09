using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class KontrAgent : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    [NotMapped]
    public KontrAgentType Type { get; set; }
    public Guid TypeId { get; set; }
    public string INN { get; set; }
    public string KPP { get; set; }
    public string OGRN { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string Address { get; set; }
    public List<User> Users { get; } = new List<User>();

    public KontrAgent()
    {

    }
    public KontrAgent(KontrAgentModel model)
    {
        Id = model.Id;
        FullName = model.FullName;
        INN = model.INN;
        KPP = model.KPP;
        OGRN = model.OGRN;
        ContactPerson = model.ContactPersonName;
        ContactPhone = model.ContactPhone;
        ContactEmail = model.ContactEmail;
        Address = model.Address;
    }
}
