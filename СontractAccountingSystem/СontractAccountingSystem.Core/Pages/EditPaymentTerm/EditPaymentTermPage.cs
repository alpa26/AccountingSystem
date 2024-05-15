using System;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.LaborHours;
using СontractAccountingSystem.Core.Pages.LaborHours1;

namespace СontractAccountingSystem.Core.Pages.EditPaymentTerm
{
    public class EditPaymentTermPage : EditFormPage<PaymentTermModel>
    {
        public LaborHoursModel[] _laborHours;
        public bool LaborsIsNew = false;

        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер документа");
        [Required]
        public DatePeriodInput Deadline { get; } = new DatePeriodInput("Срок исполнения") { Placeholder = "Введите значение" };

        public CollectionEditor<LaborHoursModel> LaborHours { get; } = new CollectionEditor<LaborHoursModel>("Отработанные часы");


        public ValueInput<decimal> Amount { get; } = new ValueInput<decimal>("Сумма", x =>
        {
            decimal result;
            if (decimal.TryParse(x, out result))
                return result;
            return 0;
        });
        [Required]
        public ComboBox<PaymentStatusEnum?> Status { get; } = new ComboBox<PaymentStatusEnum?>("Статус");
        public TextInput Comment { get; } = new TextInput("Комментарий") { Placeholder = "Комментарий", Multiline = true };


        public Button CalculateButton { get; } = new Button($"Рассчитать");


        public EditPaymentTermPage(PaymentTermModel model, string docNumb, LaborHoursModel[] laborHours = null) : base(model ?? CreateModel())
        {
            DocumentNumber.Text = docNumb;
            _laborHours = new LaborHoursModel[laborHours.Length];
            for (int i = 0; i < _laborHours.Length; i++)
            {
                _laborHours[i] = (LaborHoursModel)laborHours[i].Clone();
                _laborHours[i].Hours = 0;
                _laborHours[i].FullAmount = 0;
            }
        }

        protected override void Setup()
        {
            Content.Clear();
            Content.AddRange(DocumentNumber, Deadline, Status);
            if (_laborHours.Length!=0)
                Content.AddRange(LaborHours, CalculateButton);
            Content.AddRange(Amount, Comment);

            Title = "Срок оплаты";
            DocumentNumber.Readonly = true;
            DocumentNumber.Placeholder = "Сначала укажите номер у документа";
            if (Model.DocumentNumber != null)
                Deadline.Value = new Salazki.Presentation.Period(Model.DeadlineStart, Model.DeadlineEnd);
            Comment.Text = Model.Comment;
            Amount.Value = Model.Amount;
            Status.Value = Model.Status;

            LaborHours.Items.Clear();
            if (Model.LaborHoursWorked is null)
            {
                LaborHours.Items.AddRange(_laborHours);
            }
            else
                foreach(var item in Model.LaborHoursWorked)
                {
                    LaborHours.Items.Add(item);
                }


            LaborHours.AddNewItemButton.Text = "Указать отработанные часы";
            LaborHours.RegisterBuildItemDelegate(x => new LaborHoursItem(x));
            LaborHours.CreateItemEditPageDelegate = x => {
                return new EditLaborHoursPage(x, true);
            };

            CalculateButton.AsyncActionDelegate = async () =>
            {
                Amount.Value = 0;
                if (LaborHours.Items.Count != 0)
                    foreach (var item in LaborHours.Items)
                        Amount.Value += item.FullAmount;
            };
        }

        private static PaymentTermModel CreateModel()
        {
            return new PaymentTermModel
            {
                Id = Guid.NewGuid(),
            };
        }
    }
}
