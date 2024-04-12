using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.EditDocument
{
    public class EditLicenseDocumentPage : EditFormPage<ArchiveDocumentModel>
    {
        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер Договора") { MaxLength = 9, MaxDisplayLength = 12, Placeholder = "00-000000", DisplayTextDelegate = x => $"№ {x}" };
        
        [Required]
        public TextInput EssenceOfAgreement { get; } = new TextInput("Наименование работ") { Placeholder = "Наименование работ", Multiline = true };

        [Required]
        public DatePeriodInput Deadline { get; } = new DatePeriodInput("Срок исполнения") { Placeholder = "Введите значение" };

        [Required]
        public ValueInput<decimal> FullPrice { get; } = new ValueInput<decimal>("Общая Сумма", x =>
        {
            decimal result;
            if (decimal.TryParse(x, out result))
                return result;
            return 0;
        });
        
        [Required]
        public ComboBox<PaymentTypeEnum?> PaymentType { get; } = new ComboBox<PaymentTypeEnum?>("Тип оплаты");

        [Required]
        public KontrAgentAutocomplete KontrAgentName { get; } = new KontrAgentAutocomplete("КонтрАгент");

        [Required]
        public OrganizationAutocomplete OrganizationName { get; } = new OrganizationAutocomplete("Название организации");


        public TextInput Comment { get; } = new TextInput("Комментарий") { Placeholder = "Комментарий", Multiline = true };

        public EditLicenseDocumentPage() : this(null)
        {

        }
        public EditLicenseDocumentPage(ArchiveDocumentModel model) : base(model)
        {
            CreateModelDelegate = CreateModel;
            DeleteButton.Hidden = true;
        }

        protected override void Setup()
        {
            if (Model.DocumentNumber == null)
                Title = "Новый лицензионный договор";
            else
                Title = $"Лицензионный Договор №{Model.DocumentNumber}";

            DocumentNumber.Text = Model.DocumentNumber;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            FullPrice.Value = Model.FullPrice;
            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            Comment.Text = Model.Comment;
            PaymentType.Value = Model.PaymentType;

            FullPrice.DisplayTextDelegate = x =>
            {
                if (x == 0)
                    return null;
                return $"{x} рублей";
            };

        }

        private static ArchiveDocumentModel CreateModel()
        {
            return new ArchiveDocumentModel()
            {
            };
        }

    }
}
