using Newtonsoft.Json.Linq;
using Salazki.Presentation.Elements;
using System;
using System.ComponentModel;
using System.Reflection;

using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewDocumentPage : ViewFormPage<ArchiveDocumentModel>
    {
        internal Guid DocumentId { get; private set; }

        public TextField DocumentName { get; } = new TextField("Договор");
        public TextField Amount { get; } = new TextField("Общая сумма");

        public TextField EssenceOfAgreement { get; } = new TextField("Наименование работ");
        public TextField<DateTime> CreateDate { get; } = new TextField<DateTime>("Дата добавления");
        public TextField<DateTime> DeadlineStart { get; } = new TextField<DateTime>("Начало срока исполнения");
        public TextField<DateTime> DeadlineEnd { get; } = new TextField<DateTime>("Конец срока исполнения");

        public TextField PaymentType { get; } = new TextField("Тип оплаты");

        public TextField<PersonModel> EmployerName { get; } = new TextField<PersonModel>("Сотрудник");

        public TextField<KontrAgentModel> KontrAgentName { get; } = new TextField<KontrAgentModel>("КонтрАгент");

        public TextField<OrganizationModel> OrganizationName { get; } = new TextField<OrganizationModel>("Название организации");

        public CollectionViewer<PaymentTermModel> PaymentTerms { get; } = new CollectionViewer<PaymentTermModel>("Сроки оплаты");

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
                DeadlineStart, DeadlineEnd,
                Amount, PaymentType,
                PaymentTerms,
                KontrAgentName, OrganizationName,
                EssenceOfAgreement,
                Comment
                );
            }
            else if (Model.DocumentType == "Договор на фактические услуги")
            {
                Title = $"Договор на фактические услуги № {Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                DeadlineStart, DeadlineEnd,
                EmployerName,
                Amount, PaymentType,
                PaymentTerms,
                KontrAgentName,
                EssenceOfAgreement,
                Comment
                );
            }
            else /*if (Element.Model.DocumentType == "Лицензионный договор")*/
            {
                Title = $"Лицензионный договор № {Model.DocumentNumber}";
                Subtitle = $"Дата добавления {Model.CreateDate.ToString("dd.MM.yyyy")}";
                Content.AddRange(
                DocumentName,
                DeadlineStart, DeadlineEnd,
                OrganizationName,
                Amount, PaymentType,
                PaymentTerms,
                KontrAgentName, 
                EssenceOfAgreement,
                Comment
                );
            }


            DocumentName.Text = Model.Name;
            CreateDate.Value = Model.CreateDate;
            DeadlineStart.Value = Model.DeadlineStart;
            DeadlineEnd.Value = Model.DeadlineEnd;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            PaymentType.Text = typeof(PaymentTypeEnum)
                                .GetField(Model.PaymentType.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
            Amount.Text = $"{Model.FullPrice} рублей";
            KontrAgentName.Value = Model.KontrAgentName;
            OrganizationName.Value = Model.OrganizationName;
            EmployerName.Value = Model.WorkerName;
            Comment.Text = Model.Comment;


            PaymentTerms.Items.Clear();
            PaymentTerms.Items.AddRange(Model.PaymentTerms);
            PaymentTerms.RegisterBuildItemDelegate(x => new PaymentTermItem(x));
            PaymentTerms.EmptyText = "Нет сроков";
            PaymentTerms.CreateItemViewPageDelegate = x => new ViewPaymentTermPage(x);
        }
    }
}
