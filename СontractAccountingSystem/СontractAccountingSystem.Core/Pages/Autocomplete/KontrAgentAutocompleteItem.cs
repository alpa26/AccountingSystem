using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class KontrAgentAutocompleteItem : Item<KontrAgentModel>
    {
        public Label FullName { get; } = new Label();
        public Label INN { get; } = new Label { Style = TextStyle.LightDescription };

        public KontrAgentAutocompleteItem(KontrAgentModel model) : base(model)
        {
            Layout = new VerticalStack(FullName, INN);
        }

        protected override void Setup()
        {
            FullName.Text = Model.FullName;
            INN.Text = Model.INN;
        }
    }
}
