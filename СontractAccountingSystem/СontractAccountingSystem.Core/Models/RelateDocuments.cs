using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models;

public class RelateDocuments
{
    public Guid Id { get; set; }
    public Guid Document1Id { get; set; }
    public Document Document1 { get; set; } = new();
    public Guid Document2Id { get; set; }
    public Document Document2 { get; set; } = new();
    public RelateDocuments()
    {

    }

}
