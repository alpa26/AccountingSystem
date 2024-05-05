using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewLaborHoursPage : ViewFormPage<LaborHoursModel>
    {
        public TextField<PersonModel> WorkerName { get; } = new TextField<PersonModel>("Сотрудник");
        public TextField HourlyRate { get; } = new TextField("Почасовая ставка");
        public TextField Hours { get; } = new TextField("Отработанные часы");
        public TextField FullAmount { get; } = new TextField("Сумма");

        public ViewLaborHoursPage(LaborHoursModel model, bool IsPaymentPage) : base(model)
        {
            Content.Clear();
            Content.AddRange(WorkerName, HourlyRate);
            if (IsPaymentPage){
                Title = $"Отработанные часы сотрудника {Model.WorkerName.FullName}";
                Content.AddRange(Hours, FullAmount);
            }
            else
                Title = $"Почасовая ставка сотрудника {Model.WorkerName.FullName}";
        }

        protected override void Setup()
        {
            HourlyRate.Text = $"{Model.HourlyRate} р/ч";
            WorkerName.Value = Model.WorkerName;
            Hours.Text = $"{Model.Hours} часов";
            FullAmount.Text = $"{Model.FullAmount} рублей";

        }

    }
    
}
