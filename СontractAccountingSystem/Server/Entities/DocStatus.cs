﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Server.Entities.Interfaces;

namespace СontractAccountingSystem.Server.Entities;

public class DocStatus : IEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DocStatus() { }
    public DocStatus(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
