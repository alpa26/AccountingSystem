using System;
using Salazki.Presentation.Elements;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.LaborHours1
{
    public class LaborHoursItem : Item<LaborHoursModel>
    {

        public Label WorkerName { get; } = new Label();
        public Label HourlyRate { get; } = new Label();
        public Label StaffPosition { get; } = new Label();

        public LaborHoursItem(LaborHoursModel model) : base(model)
        {
            StaffPosition.Style = TextStyle.LightDescription;
            Layout = BuildLayout();
        }

        protected override void Setup()
        {
            WorkerName.Text = Model.WorkerName is null ? "" : Model.WorkerName.FullName;
            HourlyRate.Text = $"{Model.HourlyRate} р/ч";
            StaffPosition.Text = Model.WorkerName is null ? "" : Model.WorkerName.StaffPosition;
        }
        private Layout BuildLayout()
        {
            return new GridLayout(grid =>
            {
                grid.Add(WorkerName).Row(0).Column(0);
                grid.Add(new GridLayout(x =>
                {
                    x.Add(StaffPosition).StretchedHorizontally();
                })).Row(1).Columns(0).StretchedHorizontally();
                grid.Add(HourlyRate).Row(0).Column(1).AlignedToRight();
            });
        }
    }
}
