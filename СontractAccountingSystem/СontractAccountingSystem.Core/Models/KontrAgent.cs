using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models;

public class KontrAgent
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public KontrAgentType Type { get; set; }
    public Guid TypeId { get; set; }
    public string INN { get; set; }
    public string Address { get; set; }
    public User User { get; set; } = new();

    public KontrAgent()
    {

    }
}
