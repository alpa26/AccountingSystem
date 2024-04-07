﻿using Newtonsoft.Json.Linq;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewDocumentPage : ViewFormPage<ArchiveDocumentModel>
    {
        internal int DocumentId { get; private set; }

        public TextField DocumentName { get; } = new TextField("Договор");
        public TextField FullPrice { get; } = new TextField("Общая сумма");

        public TextField EssenceOfAgreement { get; } = new TextField("Наименование работ");
        public TextField<DateTime> CreateDate { get; } = new TextField<DateTime>("Дата согласования");
        public TextField<DateTime> DeadlineStart { get; } = new TextField<DateTime>("Начало срока исполнения");
        public TextField<DateTime> DeadlineEnd { get; } = new TextField<DateTime>("Конец срока исполнения");

        public TextField PaymentType { get; } = new TextField("Тип оплаты");

        public TextField<PersonModel> EmployerName { get; } = new TextField<PersonModel>("Сотрудник");

        public TextField<KontrAgentModel> KontrAgentName { get; } = new TextField<KontrAgentModel>("КонтрАгент");

        public TextField<OrganizationModel> OrganizationName { get; } = new TextField<OrganizationModel>("Название организации");

        public TextField Comment { get; } = new TextField("Комментарий");

        public Button EditButton { get; } = new Button { Icon = IconType.Pencil, Hint = "Редактировать" };

        public ViewDocumentPage(int Id)
        {
            DocumentId = Id ;
            HeaderActionPanel.Buttons.AddRange(EditButton);

            Content.AddRange(
                DocumentName,
                CreateDate,
                DeadlineStart,
                DeadlineEnd,
                EssenceOfAgreement,
                PaymentType,
                KontrAgentName,
                OrganizationName,
                EmployerName,
                Comment
                );
        }


        protected override void Setup()
        {
            Title = $"Документ {Model.DocumentNumber}";
            DocumentName.Text = Model.Name;
            CreateDate.Value = Model.CreateDate;
            DeadlineStart.Value = Model.DeadlineStart;
            DeadlineEnd.Value = Model.DeadlineEnd;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            PaymentType.Text = typeof(PaymentTypeEnum)
                                .GetField(Model.PaymentType.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;

            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            EmployerName.Value = Model.EmployerName;
            Comment.Text = Model.Comment;
        }
    }
}
