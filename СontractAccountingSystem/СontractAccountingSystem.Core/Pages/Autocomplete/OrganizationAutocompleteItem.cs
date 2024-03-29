using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class OrganizationAutocompleteItem : Item<OrganizationModel>
    {
        public Label Name { get; } = new Label();

        public OrganizationAutocompleteItem(OrganizationModel model) : base(model)
        {
            Layout = new VerticalStack(Name);
        }

        protected override void Setup()
        {
            Name.Text = Model.Name;
        }
    }
}
