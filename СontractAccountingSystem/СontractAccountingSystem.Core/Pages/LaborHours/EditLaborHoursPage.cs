using System;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.LaborHours
{
    public class EditLaborHoursPage : EditFormPage<LaborHoursModel>
    {
        [Required]
        public WorkerAutocomplete WorkerName { get; } = new WorkerAutocomplete("Рабочий");

        public ValueInput<decimal> HourlyRate { get; } = new ValueInput<decimal>("Рублей в час", x =>
        {
            decimal result;
            if (decimal.TryParse(x, out result))
                return result;
            return 0;
        });
        public ValueInput<int> Hours { get; } = new ValueInput<int>("Отработанные часы", x =>
        {
            int result;
            if (int.TryParse(x, out result))
                return result;
            return 0;
        });

        public Button CalculateButton { get; } = new Button($"Рассчитать");


        public ValueInput<decimal> FullAmount { get; } = new ValueInput<decimal>("Итого", x =>
        {
            int result;
            if (int.TryParse(x, out result))
                return result;
            return 0;
        });

        public EditLaborHoursPage(LaborHoursModel model, bool IsPaymentPage, bool isNew=false) : base(model ?? CreateModel())
        {
            Content.Clear();
            Content.AddRange(WorkerName, HourlyRate);
            if (IsPaymentPage)
            {
                WorkerName.Readonly = true;
                HourlyRate.Readonly = true;
                FullAmount.Readonly = true;
                Content.AddRange(Hours, CalculateButton, FullAmount);
            }
        }

        protected override void Setup()
        {
            Title = "Трудозатраты";
            WorkerName.Value = Model.WorkerName;
            HourlyRate.Value = Model.HourlyRate;
            Hours.Value = Model.Hours;
            FullAmount.Value = Model.FullAmount;

            Hours.DisplayTextDelegate = x =>
            {
                if (x == 0)
                    return null;
                return $"{x} часов";
            };

            CalculateButton.AsyncActionDelegate = async () =>
            {
                FullAmount.Value = HourlyRate.Value * Hours.Value;
            };
        }

        private static LaborHoursModel CreateModel()
        {
            return new LaborHoursModel
            {
                Id = Guid.NewGuid(),
            };
        }
    }
}
