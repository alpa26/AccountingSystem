using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class DocumentAutocompleteItem : Item<RelateDocumentModel>
    {
        public Label Name { get; } = new Label();
        public Label DocumentNumber { get; } = new Label { Style = TextStyle.LightDescription };

        public DocumentAutocompleteItem(RelateDocumentModel model) : base(model)
        {
            Layout = new VerticalStack(Name, DocumentNumber);
        }

        protected override void Setup()
        {
            Name.Text = Model.DocumentName;
            DocumentNumber.Text = $"№{Model.DocumentNumber}";
        }
    }
}
