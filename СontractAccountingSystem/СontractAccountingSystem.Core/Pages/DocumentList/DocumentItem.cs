using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.DocumentList
{
    public class DocumentItem : Item<DocumentListItemModel>
    {
        public Label DocumentNumber { get; } = new Label { Length = 9 };
        public Label<DateTime> CreateDate { get; } = new Label<DateTime>();
        public Label KontrAgentName { get; } = new Label();
        public Label DocumentType { get; } = new Label();
        public BadgesCollection Badges { get; } = new BadgesCollection();
        public Label EssenceOfAgreement { get; } = new Label();
        public Label Name { get; } = new Label();



        public DocumentItem(DocumentListItemModel model) : base(model)
        {
            CreateDate.TextAlignment = TextAlignment.Right;
            CreateDate.Formatter = Formatters.HistoricalDateTimeFormatter;
            EssenceOfAgreement.Style = TextStyle.LightDescription;
            KontrAgentName.Style = TextStyle.LightDescription;

            Layout = BuildLayout();
        }

        protected override void Setup()
        {
            DocumentNumber.Text = "№{0}".FormatWith(Model.DocumentNumber);
            CreateDate.Value = Model.CreateDate;
            KontrAgentName.Text = Model.KontrAgentName.FullName;
            EssenceOfAgreement.Text = Model.EssenceOfAgreement;
            Name.Text = Model.Name;

            if (Model.DocumentType == "Договор на фактические услуги")
                DocumentType.Text = "Договор на усл.";
            else if(Model.DocumentType == "Лицензионный договор")
                DocumentType.Text = "Договор на лиц.";
            else 
                DocumentType.Text = "Договор на раб.";
            Badges.Items.Clear();
            Badges.Items.AddRange(
                new Badge
                {
                    Text = "На согласовании",
                    Color =  BadgeColor.Primary
                });
        }

        private Layout BuildLayout()
        {
            return new GridLayout(grid =>
            {
                grid.Add(DocumentNumber).Row(0).Column(0);
                grid.Add(DocumentType).Row(0).Column(1);
                //grid.Add(KontrAgentName).Row(0).Column(2);
                grid.Add(Name).Row(0).Column(2);
                grid.Add(new HorizontalStack(Badges, CreateDate)).Row(0).Columns(from:3,to:4).AlignedToRight();
                grid.Add(new GridLayout(x =>
                {
                    x.Add(KontrAgentName).StretchedHorizontally();
                })).Row(1).Columns(from: 1, to: 2).StretchedHorizontally();
                grid.Add(new GridLayout(x =>
                {
                    x.Add(EssenceOfAgreement).StretchedHorizontally();
                })).Row(1).Column(2).StretchedHorizontally();
            });
        }
    }
}
