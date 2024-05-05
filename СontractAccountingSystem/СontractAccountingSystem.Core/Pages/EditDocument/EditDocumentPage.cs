﻿using Salazki.Presentation.Elements;
using System;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using СontractAccountingSystem.Core.Pages.LaborHours;
using СontractAccountingSystem.Core.Pages.LaborHours1;

namespace СontractAccountingSystem.Core.Pages.EditDocument
{
    public class EditDocumentPage : EditFormPage<ArchiveDocumentModel>
    {
        internal string Type { get; set; }
        internal bool IsNew { get; set; } = false;

        [Required]
        public TextInput DocumentNumber { get; } = new TextInput("Номер Договора") { MaxLength = 7, MaxDisplayLength = 12, Placeholder = "0000-00", DisplayTextDelegate = x => $"№ {x}" };

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

        public CollectionEditor<PaymentTermModel> PaymentTerms { get; } = new CollectionEditor<PaymentTermModel>("Сроки оплаты");

        public CollectionEditor<LaborHoursModel> LaborHours { get; } = new CollectionEditor<LaborHoursModel>("Сроки оплаты");

        public EditDocumentPage(string type) : this(null, type)
        {

        }
        public EditDocumentPage(ArchiveDocumentModel model, string type) : base(model)
        {
            CreateModelDelegate = CreateModel;
            DeleteButton.Hidden = true;
            Type = type;


            LaborHours.AddNewItemButton.Text = "Добавить сотрудника";
            LaborHours.RegisterBuildItemDelegate(x => new LaborHoursItem(x));
            LaborHours.CreateItemEditPageDelegate = x => new EditLaborHoursPage(x, false);


            PaymentTerms.AddNewItemButton.Text = "Добавить дату оплаты";
            PaymentTerms.RegisterBuildItemDelegate(x => new PaymentTermItem(x));
            PaymentTerms.CreateItemEditPageDelegate = x =>
                 new EditPaymentTermPage(x, DocumentNumber.Value, LaborHours.Items.ToArray());
            //PaymentTerms.CreateItemEditPageDelegate = x => 
            //     new EditPaymentTermPage(x, DocumentNumber.Value, LaborHours.Items.Select(x=> {x.Hours=0; x.FullAmount = 0; return x; }).ToArray());
        }

        protected override void Setup()
        {
            Content.Clear();
            if (Type == "Договор на работы")
            {
                Content.AddRange(
                DocumentNumber,
                Deadline,
                FullPrice, PaymentType, PaymentTerms,
                KontrAgentName, OrganizationName,
                EssenceOfAgreement, Comment
                );
            }
            if (Type == "Договор на фактические услуги")
            {
                Content.AddRange(
                DocumentNumber,
                Deadline, LaborHours,
                FullPrice, PaymentType, PaymentTerms,
                KontrAgentName,
                EssenceOfAgreement, Comment
                );
            }
            if (Type == "Лицензионный договор")
            {
                Content.AddRange(
                DocumentNumber, EssenceOfAgreement,
                Deadline,
                FullPrice, PaymentType, PaymentTerms,
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
            if (Model.DocumentNumber != null)
                Deadline.Value = new Salazki.Presentation.Period(Model.DeadlineStart, Model.DeadlineEnd);


            FullPrice.DisplayTextDelegate = x =>
            {
                if (x == 0)
                    return null;
                return $"{x} рублей";
            };
            LaborHours.Items.Clear();
            LaborHours.Items.AddRange(Model.LaborHoursCost);

            PaymentTerms.Items.Clear();
            PaymentTerms.Items.AddRange(Model.PaymentTerms);
        }

        private ArchiveDocumentModel CreateModel()
        {
            IsNew = true;
            return new ArchiveDocumentModel()
            {
                Id = new Guid()
            };
        }
    }
}
