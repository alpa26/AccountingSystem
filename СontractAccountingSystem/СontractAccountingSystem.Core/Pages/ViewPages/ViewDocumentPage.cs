﻿using Newtonsoft.Json.Linq;
using Salazki.Presentation.Elements;
using System;
using System.ComponentModel;
using System.Reflection;

using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;
using СontractAccountingSystem.Core.Pages.EditPaymentTerm;
using СontractAccountingSystem.Core.Pages.LaborHours;
using СontractAccountingSystem.Core.Pages.LaborHours1;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewDocumentPage : ViewFormPage<ArchiveDocumentModel>
    {
        internal Guid DocumentId { get; private set; }

        public TextField DocumentName { get; } = new TextField("Договор");
        public TextField Amount { get; } = new TextField("Общая сумма");

        public TextField EssenceOfAgreement { get; } = new TextField("Наименование работ");
        public TextField<DateTime> CreateDate { get; } = new TextField<DateTime>("Дата добавления");
        public TextField<string> Deadline { get; } = new TextField<string>("Сроки исполнения");
        public TextField<DateTime> DeadlineStart { get; } = new TextField<DateTime>("Начало срока исполнения");
        public TextField<DateTime> DeadlineEnd { get; } = new TextField<DateTime>("Конец срока исполнения");
        public TextField Status { get; } = new TextField("Статус");
        public TextField PaymentType { get; } = new TextField("Тип оплаты");

        public TextField<PersonModel> WorkerName { get; } = new TextField<PersonModel>("Сотрудник");

        public TextField<KontrAgentModel> KontrAgentName { get; } = new TextField<KontrAgentModel>("КонтрАгент");

        public TextField<OrganizationModel> OrganizationName { get; } = new TextField<OrganizationModel>("Исполнитель");

        public CollectionViewer<PaymentTermModel> PaymentTerms { get; } = new CollectionViewer<PaymentTermModel>("Сроки оплаты");

        public CollectionViewer<LaborHoursModel> LaborHours { get; } = new CollectionViewer<LaborHoursModel>("Стоимость трудозатрат");

        public CollectionViewer<RelateDocumentModel> RelateDocuments { get; } = new CollectionViewer<RelateDocumentModel>("Сопроводительные документы");


        public TextField Comment { get; } = new TextField("Комментарий");

        public Button EditButton { get; } = new Button { Icon = IconType.Pencil, Hint = "Редактировать" };
        public ShowActionFormButton DeleteButton { get; } = new ShowActionFormButton { Text = "Удалить", Style = ButtonStyle.Danger };


        public ViewDocumentPage(Guid Id)
        {
            DocumentId = Id;
            HeaderActionPanel.Buttons.AddRange(EditButton,DeleteButton);
        }


        protected override void Setup()
        {

            Content.Clear();
            if (Model.DocumentType == "Договор на работы")
            {
                Title = $"Договор на работы №{Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                Deadline,
                Status,
                KontrAgentName, OrganizationName,
                RelateDocuments,
                Comment
                );
            }
            else if (Model.DocumentType == "Дополнительное соглашение к договору на раб.")
            {
                if (Model.DocumentType == "Дополнительное соглашение к договору на раб." && Model.RelatedDocuments.Length == 1)
                    Title = $"Дополнительное соглашение №{Model.DocumentNumber} к договору на раб. № {Model.RelatedDocuments[0].DocumentNumber}";
                else
                    Title = $"Дополнительное соглашение №{Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                Deadline,
                Status,
                KontrAgentName, OrganizationName,
                PaymentType, PaymentTerms, Amount,
                EssenceOfAgreement, RelateDocuments,
                Comment
                );
            }
            else if (Model.DocumentType == "Договор на фактические услуги" || Model.DocumentType == "Дополнительное соглашение к договору на усл.")
            {
                if(Model.DocumentType == "Дополнительное соглашение к договору на усл." && Model.RelatedDocuments.Length == 1)
                {
                    if(Model.RelatedDocuments.Length == 1)
                        Title = $"Дополнительное соглашение № {Model.DocumentNumber} к договору на усл. № {Model.RelatedDocuments[0].DocumentNumber}";
                    else
                        Title = $"Дополнительное соглашение № {Model.DocumentNumber}";
                }
                else 
                    Title = $"{Model.DocumentType} № {Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                Deadline, Status, KontrAgentName,
                LaborHours,
                PaymentType, PaymentTerms, Amount,
                RelateDocuments, EssenceOfAgreement,
                Comment
                );
            }
            else /*if (Element.Model.DocumentType == "Лицензионный договор")*/
            {
                if (Model.DocumentType == "Дополнительное соглашение к договору на лиц." && Model.RelatedDocuments.Length == 1)
                {
                    if (Model.RelatedDocuments.Length == 1)
                        Title = $"Дополнительное соглашение № {Model.DocumentNumber} к договору на лиц. № {Model.RelatedDocuments[0].DocumentNumber}";
                    else
                        Title = $"Дополнительное соглашение №{Model.DocumentNumber}";
                }
                else
                    Title = $"{Model.DocumentType} № {Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                Deadline, Status,
                KontrAgentName, OrganizationName,
                PaymentType, PaymentTerms, Amount,
                RelateDocuments, EssenceOfAgreement,
                Comment
                );
            }


            DocumentName.Text = Model.Name;
            CreateDate.Value = Model.CreateDate;
            Deadline.Value = $"с {Model.DeadlineStart.ToString("d")} по {Model.DeadlineEnd.ToString("d")}";

            //DeadlineStart.Value = Model.DeadlineStart;
            //DeadlineEnd.Value = Model.DeadlineEnd;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            Status.Text = typeof(DocStatusEnum)
                                .GetField(Model.Status.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
            PaymentType.Text = typeof(PaymentTypeEnum)
                                .GetField(Model.PaymentType.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
            Amount.Text = $"{Model.FullPrice} рублей";
            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            WorkerName.Value = Model.WorkerName;
            Comment.Text = Model.Comment;

            LaborHours.Items.Clear();
            LaborHours.Items.AddRange(Model.LaborHoursCost);
            LaborHours.RegisterBuildItemDelegate(x => new LaborHoursItem(x));
            LaborHours.EmptyText = "Не указано";
            LaborHours.CreateItemViewPageDelegate = x => new ViewLaborHoursPage(x, false);

            PaymentTerms.Items.Clear();
            PaymentTerms.Items.AddRange(Model.PaymentTerms);
            PaymentTerms.RegisterBuildItemDelegate(x => new PaymentTermItem(x));
            PaymentTerms.EmptyText = "Нет сроков";
            PaymentTerms.CreateItemViewPageDelegate = x => new ViewPaymentTermPage(x);

            RelateDocuments.Items.Clear();
            RelateDocuments.Items.AddRange(Model.RelatedDocuments);
            RelateDocuments.RegisterBuildItemDelegate(x => new DocumentAutocompleteItem(x));
            RelateDocuments.EmptyText = "Нет прикрепленных документов";
            RelateDocuments.CreateItemViewPageDelegate = x => new ViewDocumentPage(x.RelatedDocumentId);
        }
    }
}
