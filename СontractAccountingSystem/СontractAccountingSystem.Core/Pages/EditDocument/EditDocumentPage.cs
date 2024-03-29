using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.EditDocument
{
    public class EditDocumentPage : EditFormPage<ArchiveDocumentModel>
    {
        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер Договора") { MaxLength = 9, MaxDisplayLength = 12, Placeholder = "00-000000", DisplayTextDelegate = x => $"№ {x}" };
        

        [Required]
        public TextInput EssenceOfAgreement { get; } = new TextInput("Наименование работ") { Placeholder = "Наименование работ", Multiline = true };

        //[Required]
        public DateInput CreateDate { get; } = new DateTimeInput("Дата согласования");

        [Required]
        public DatePeriodInput Deadline { get; } = new DatePeriodInput("Срок исполнения") { Placeholder = "Введите значение" };

        [Required]
        public ComboBox<PaymentTypeEnum?> PaymentType { get; } = new ComboBox<PaymentTypeEnum?>("Тип оплаты");

        [Required]
        public EmployeeAutocomplete EmployerName { get; } = new EmployeeAutocomplete("Сотрудник");

        [Required]
        public KontrAgentAutocomplete KontrAgentName { get; } = new KontrAgentAutocomplete("КонтрАгент");

        [Required]
        public OrganizationAutocomplete OrganizationName { get; } = new OrganizationAutocomplete("Название организации");

        [Required]
        public ValueInput<decimal> FullPrice { get; } = new ValueInput<decimal>("Общая Сумма", x =>
        {
            decimal result;
            if (decimal.TryParse(x, out result))
                return result;
            return 0;
        });


        public TextInput Comment { get; } = new TextInput("Комментарий") { Placeholder = "Комментарий", Multiline = true };

        //public MultiValueAutocomplete<PersonModel> Signers2 { get; } = new MultiValueAutocomplete<PersonModel>("Согласователи2")
        //{
        //    BuildAutocompleteDelegate = () => new EmployeeAutocomplete("тест") { Placeholder = "Введите имя сотрудника или должность" },
        //    ValuesCountLimit = 3
        //};

        //public AttachmentsEditor Documents { get; } = new AttachmentsEditor("Документы")
        //{
        //    //MimeTypes = MimeType.Document | MimeType.Image,
        //    AttachmentsQuantityLimit = 2,
        //    //OpenMode = AttachmentOpenMode.DocumentModifier
        //};


        public EditDocumentPage() : this(null)
        {

        }
        public EditDocumentPage(ArchiveDocumentModel model) : base(model)
        {
            CreateModelDelegate = CreateModel;
            DeleteButton.Hidden = true;
        }

        protected override void Setup()
        {
            if (Model.DocumentNumber == null)
                Title = "Новый договор";
            else
                Title = $"Договор  {Model.DocumentNumber}";

            DocumentNumber.Text = Model.DocumentNumber;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            FullPrice.Value = Model.FullPrice;
            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            EmployerName.Value = Model.EmployerName;
            Comment.Text = Model.Comment;
            PaymentType.Value = Model.PaymentType;
            CreateDate.Value = Model.CreateDate;

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
