using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public enum RoleEnum
    {
        [Description("Администратор")] admin,
        [Description("Директор")] director,
        [Description("Руководитель")] user
    }
}
