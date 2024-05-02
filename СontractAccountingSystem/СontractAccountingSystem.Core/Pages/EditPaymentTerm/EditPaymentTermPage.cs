using System;
using Salazki.Presentation.Elements;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.EditPaymentTerm
{
    public class EditPaymentTermPage : EditFormPage<PaymentTermModel>
    {
        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер документа");
        [Required]
        public DatePeriodInput Deadline { get; } = new DatePeriodInput("Срок исполнения") { Placeholder = "Введите значение" };
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


        //public TextInput ChangeNumber { get; } = new TextInput("Номер извещения");
        //public DateInput AdoptionDate { get; } = new DateInput("Дата утверждения");
        //public AttachmentsEditor Documents { get; } = new AttachmentsEditor("Электронная копия")
        //{
        //    //MimeTypes = MimeType.Document | MimeType.Image,
        //    AttachmentsQuantityLimit = 1
        //};

        public EditPaymentTermPage(PaymentTermModel model, string docNumb) : base(model ?? CreateModel())
        {
            DocumentNumber.Text = docNumb;
        }

        protected override void Setup()
        {
            Title = "Срок оплаты";
            DocumentNumber.Readonly = true;
            DocumentNumber.Placeholder = "Сначала укажите номер у документа";
            if (Model.DocumentNumber != null)
                Deadline.Value = new Salazki.Presentation.Period(Model.DeadlineStart, Model.DeadlineEnd);
            Comment.Text = Model.Comment;
            Amount.Value = Model.Amount;
            Status.Value = Model.Status;
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
