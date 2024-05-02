using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Models
{
    public enum PaymentStatusEnum
    {
        [Description("Расчет")] Сalculation,
        [Description("Ожидает оплаты")] AwaitingPayment,
        [Description("Оплачено")] PaidFor,
        [Description("Просрочено")] Overdue
    }
}
