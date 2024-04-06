using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public enum PaymentTypeEnum
    {
        [Description("ПредОплата")] FullPrePayment,
        [Description("ПостОплата")] FullPostPayment,
        [Description("Частичная")] Partial,
        [Description("Ежемесячно")] Monthly
    }
}
