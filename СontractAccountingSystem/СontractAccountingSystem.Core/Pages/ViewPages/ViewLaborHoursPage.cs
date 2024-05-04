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
        public TextField HourlyRate { get; } = new TextField("За час");
        public TextField Hours { get; } = new TextField("Часы за месяц");

        public ViewLaborHoursPage(LaborHoursModel model) : base(model)
        {

        }

        protected override void Setup()
        {
            Title = $"Трудозатраты сотрудника {Model.WorkerName.FullName}";

            Content.Clear();
            Content.AddRange(WorkerName, HourlyRate);
            if (Model.Hours != 0)
                Content.AddRange(Hours);


            HourlyRate.Text = $"{Model.HourlyRate} рублей";
            WorkerName.Value = Model.WorkerName;
            Hours.Text = Model.Hours.ToString();
        }

    }
    
}
