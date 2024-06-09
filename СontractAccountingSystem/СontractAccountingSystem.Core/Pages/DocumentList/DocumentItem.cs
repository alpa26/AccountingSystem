using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace СontractAccountingSystem.Core.Pages.DocumentList
{
    public class DocumentItem : Item<DocumentListItemModel>
    {
        public Label DocumentNumber { get; } = new Label { Length = 9 };
        public Label<DateTime> CreateDate { get; } = new Label<DateTime>();
        public Label KontrAgentName { get; } = new Label();
        public Label DocumentType { get; } = new Label();
        public BadgesCollection Badges { get; } = new BadgesCollection();
        public Label Deadline { get; } = new Label();
        public Label Name { get; } = new Label();



        public DocumentItem(DocumentListItemModel model) : base(model)
        {
            CreateDate.TextAlignment = TextAlignment.Right;
            CreateDate.Formatter = Formatters.HistoricalDateTimeFormatter;
            Deadline.Style = TextStyle.LightDescription;
            KontrAgentName.Style = TextStyle.LightDescription;

            Layout = BuildLayout();
        }

        protected override void Setup()
        {
            DocumentNumber.Text = "№{0}".FormatWith(Model.DocumentNumber);
            CreateDate.Value = Model.CreateDate;
            KontrAgentName.Text = Model.KontrAgentName.FullName;
            Deadline.Text = $"с {Model.DeadlineStart.ToString("d")} по {Model.DeadlineEnd.ToString("d")}";

            Name.Text = Model.Name;

            if (Model.DocumentType == "Договор на фактические услуги")
                DocumentType.Text = "Договор  на  усл.";
            else if(Model.DocumentType == "Лицензионный договор")
                DocumentType.Text = "Договор  на  лиц.";
            else if (Model.DocumentType == "Договор на работы")
                DocumentType.Text = "Договор  на  раб.";
            else if(Model.DocumentType == "Дополнительное соглашение к договору на раб.")
                DocumentType.Text = "Д.С.  к  Д.  на раб.";
            else if (Model.DocumentType == "Дополнительное соглашение к договору на усл.")
                DocumentType.Text = "Д.С.  к  Д.  на усл.";
            else if (Model.DocumentType == "Дополнительное соглашение к договору на лиц.")
                DocumentType.Text = "Д.С.  к  Д.  на лиц.";
            Badges.Items.Clear();


            if (DateTime.Now > Model.DeadlineEnd && Model.Status != DocStatusEnum.Expired || Model.Status == DocStatusEnum.Expired)
                Badges.Items.AddRange(new Badge
                {
                    Text = "Завершен",
                    Color = BadgeColor.Default
                }
                );
            else
            {
                string text = typeof(DocStatusEnum)
                                .GetField(Model.Status.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
                BadgeColor color = BadgeColor.Default;
                if (Model.Status == DocStatusEnum.Active)
                    color = BadgeColor.Success;
                else if (Model.Status == DocStatusEnum.CustomerApproval || Model.Status == DocStatusEnum.Calculation)
                    color = BadgeColor.Primary;
                else if(Model.Status == DocStatusEnum.Completed)
                    color = BadgeColor.Default;
                Badges.Items.AddRange(new Badge
                {
                    Text = text,
                    Color = color
                }
                );
            }
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
                    x.Add(Deadline).StretchedHorizontally();
                })).Row(1).Column(2).StretchedHorizontally();
            });
        }
    }
}
