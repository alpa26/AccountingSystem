using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class KontrAgent : IEntity
{
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public KontrAgentType Type { get; set; }
    public int TypeId { get; set; }
    public string INN { get; set; }
    public string KPP { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string Address { get; set; }

    public KontrAgent()
    {

    }
}
