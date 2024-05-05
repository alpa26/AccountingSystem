using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class WorkerAutocompleteItem : Item<PersonModel>
    {
        public Label FullName { get; } = new Label();
        public Label StaffPosition { get; } = new Label { Style = TextStyle.LightDescription };

        public WorkerAutocompleteItem(PersonModel model) : base(model)
        {
            Layout = new VerticalStack(FullName, StaffPosition);
        }

        protected override void Setup()
        {
            FullName.Text = Model.FullName;
            StaffPosition.Text = Model.StaffPosition;
        }
    }
}
