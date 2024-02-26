using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models;

public class KontrAgentType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "null";
    public KontrAgentType()
    {

    }
}
