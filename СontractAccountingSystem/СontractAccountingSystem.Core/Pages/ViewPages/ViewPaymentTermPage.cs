using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewPaymentTermPage : ViewFormPage<PaymentTermModel>
    {
        public TextField DocumentNumber { get; } = new TextField("Номер договора");

        public TextField<DateTime> DeadlineStart { get; } = new TextField<DateTime>("Начало срока");
        public TextField<DateTime> DeadlineEnd { get; } = new TextField<DateTime>("Конец срока");

        public TextField Amount { get; } = new TextField("Общая сумма");
        public TextField Status { get; } = new TextField("Статус");

        public TextField Comment { get; } = new TextField("Комментарий");

        public ViewPaymentTermPage(PaymentTermModel model) : base(model)
        {

        }

        protected override void Setup()
        {
            Title = $"Оплата к договору № {Model.DocumentNumber}";

            DocumentNumber.Text = Model.DocumentNumber;
            DeadlineStart.Value = Model.DeadlineStart;
            DeadlineEnd.Value = Model.DeadlineEnd;
            Status.Text = typeof(PaymentStatusEnum)
                                .GetField(Model.Status.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
            Amount.Text = Model.Amount == decimal.Zero ? "Сумма не указана" : $"{Model.Amount} рублей";
            Comment.Text = Model.Comment =="" ? "Комментариев нет" : Model.Comment;
        }
    }
}
