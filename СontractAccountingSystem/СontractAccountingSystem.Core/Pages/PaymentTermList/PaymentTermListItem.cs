using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.PaymentTermList
{
    public class PaymentTermListItem : Item<PaymentTermModel>
    {
        public Label Price { get; } = new Label();
        public Label DocumentName { get; } = new Label();
        public Label DocumentNumber { get; } = new Label();

        public BadgesCollection Badges { get; } = new BadgesCollection();
        public Label AdoptionDate { get; } = new Label<DateTime>();


        public PaymentTermListItem(PaymentTermModel model) : base(model)
        {
            AdoptionDate.Style = TextStyle.LightDescription;
            DocumentNumber.Style = TextStyle.LightDescription;
            Badges.Items.Clear();
            var badge = new Badge();

            if (model.DeadlineEnd < DateTime.Now && model.Status != PaymentStatusEnum.PaidFor || model.Status == PaymentStatusEnum.Expired)
                Badges.Items.AddRange(badge,
                    new Badge
                    {
                        Text = "Просрочено",
                        Color = BadgeColor.Danger
                    });
            else if(model.Status == PaymentStatusEnum.AwaitingPayment)
                Badges.Items.AddRange(badge,
                    new Badge
                    {
                        Text = "Ожидает оплаты",
                        Color = BadgeColor.Warning
                    });
            else if (model.Status == PaymentStatusEnum.PaidFor)
                Badges.Items.AddRange(badge,
                    new Badge
                    {
                        Text = "Оплачено",
                        Color = BadgeColor.Success
                    });
            else if (model.Amount == 0 || model.Status == PaymentStatusEnum.Сalculation)
                Badges.Items.AddRange(badge,
                    new Badge
                    {
                        Text = "Расчет",
                        Color = BadgeColor.Primary
                    });
            Layout = BuildLayout();
        }

        protected override void Setup()
        {
            Price.Text = "Сумма {0}".FormatWith(Model.Amount == 0 ? "-" : Model.Amount);
            AdoptionDate.Text = $"с {Model.DeadlineStart.ToString("dd MMMM yyyy")} до {Model.DeadlineEnd.ToString("dd MMMM yyyy")}";
            DocumentName.Text = Model.DocumentName;
            DocumentNumber.Text = $"Документ №{Model.DocumentNumber}";
        }

        private Layout BuildLayout()
        {
            return new GridLayout(grid =>
            {
                grid.Add(DocumentName).Row(0).Columns(from: 0, to: 1);
                grid.Add(Price).Row(0).Column(2);
                grid.Add(new HorizontalStack(Badges)).Row(0).Columns(from:3,to:4).AlignedToRight();
                grid.Add(new GridLayout(x =>
                {
                    x.Add(DocumentNumber).StretchedHorizontally(); ;
                })).Row(1).Columns(from: 0, to: 1).StretchedHorizontally();
                grid.Add(new GridLayout(x =>
                {
                    x.Add(AdoptionDate).StretchedHorizontally();
                })).Row(1).Column(2).StretchedHorizontally();
            });
        }
    }
}
