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
    public class EditDocumentPage : EditFormPage<ArchiveDocumentModel>
    {
        internal string Type { get; set; }

        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер Договора") { MaxLength = 9, MaxDisplayLength = 12, Placeholder = "00-000000", DisplayTextDelegate = x => $"№ {x}" };

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
        public WorkerAutocomplete WorkerName { get; } = new WorkerAutocomplete("Рабочий");

        [Required]
        public KontrAgentAutocomplete KontrAgentName { get; } = new KontrAgentAutocomplete("КонтрАгент");

        [Required]
        public OrganizationAutocomplete OrganizationName { get; } = new OrganizationAutocomplete("Название организации");

        public TextInput EssenceOfAgreement { get; } = new TextInput("Наименование работ") { Placeholder = "Наименование работ", Multiline = true };

        public TextInput Comment { get; } = new TextInput("Комментарий") { Placeholder = "Комментарий", Multiline = true };

        //public AttachmentsEditor Documents { get; } = new AttachmentsEditor("Документы")
        //{
        //    //MimeTypes = MimeType.Document | MimeType.Image,
        //    AttachmentsQuantityLimit = 2,
        //    //OpenMode = AttachmentOpenMode.DocumentModifier
        //};

        public EditDocumentPage(string type) : this(null, type)
        {

        }
        public EditDocumentPage(ArchiveDocumentModel model, string type) : base(model)
        {
            CreateModelDelegate = CreateModel;
            DeleteButton.Hidden = true;
            Type = type;
        }

        protected override void Setup()
        {
            Content.Clear();
            if (Type == "Договор на работы")
            {
                Content.AddRange(
                DocumentNumber,
                Deadline,
                FullPrice, PaymentType,
                KontrAgentName, OrganizationName,
                EssenceOfAgreement,Comment
                );
            }
            if (Type == "Договор на фактические услуги")
            {
                Content.AddRange(
                DocumentNumber,
                Deadline, WorkerName,
                FullPrice, PaymentType,
                KontrAgentName,
                EssenceOfAgreement, Comment
                );
            }
            if (Type == "Лицензионный договор")
            {
                Content.AddRange(
                DocumentNumber, EssenceOfAgreement,
                Deadline,
                FullPrice, PaymentType,
                KontrAgentName, OrganizationName,
                Comment
                );
            }
            if (Model.DocumentNumber == null)
                Title = $"Новый {Type.ToLower()}";
            else
                Title = $"{Type} №{Model.DocumentNumber}";

            DocumentNumber.Text = Model.DocumentNumber;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            FullPrice.Value = Model.FullPrice;
            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            Comment.Text = Model.Comment;
            PaymentType.Value = Model.PaymentType;
            WorkerName.Value = Model.WorkerName;


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
