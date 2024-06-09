using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public enum DocStatusEnum
    {
        [Description("Активен")] Active,
        [Description("На согласовании у клиента")] CustomerApproval,
        [Description("Расчет/Согласование нами")] Calculation,
        [Description("Завершен")] Completed,
        [Description("Просрочен")] Expired
    }
}
