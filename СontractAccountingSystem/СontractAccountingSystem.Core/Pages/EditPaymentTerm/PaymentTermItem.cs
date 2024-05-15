using System;
using Salazki.Presentation.Elements;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using System.Data;

namespace СontractAccountingSystem.Core.Pages.EditPaymentTerm
{
    public class PaymentTermItem : Item<PaymentTermModel>
    {
        public Label Price { get; } = new Label();
        public Label Author { get; } = new Label();
        public BadgesCollection Badges { get; } = new BadgesCollection();
        public Label AdoptionDate { get; } = new Label<DateTime>();

        public PaymentTermItem(PaymentTermModel model) : base(model)
        {
            AdoptionDate.Style = TextStyle.LightDescription;
            Badges.Items.Clear();
            var badge = new Badge();
            if (model.Status == PaymentStatusEnum.AwaitingPayment)
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
            else if (model.DeadlineEnd < DateTime.Now && model.Status != PaymentStatusEnum.PaidFor)
                Badges.Items.AddRange(badge,
                    new Badge
                    {
                        Text = "Просрочено",
                        Color = BadgeColor.Danger
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
        }

        private Layout BuildLayout()
        {
            return new GridLayout(grid =>
            {
                grid.Add(Price).Row(0).Column(0);
                grid.Add(new GridLayout(x =>
                {
                    x.Add(AdoptionDate).StretchedHorizontally();
                })).Row(1).Columns(0).StretchedHorizontally();
                grid.Add(new HorizontalStack(Badges)).Row(0).Column(1).AlignedToRight();
            });
        }
    }
}
