using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.KontrAgentList
{
    public class KontrAgentListItem : Item<KontrAgentModel>
    {
        public Label FullName { get; } = new Label();
        public Label INN { get; } = new Label { Style = TextStyle.LightDescription };

        public KontrAgentListItem(KontrAgentModel model) : base(model)
        {
            Layout = new VerticalStack(FullName, INN);
        }

        protected override void Setup()
        {
            FullName.Text = Model.FullName;
            INN.Text =$"ИНН: {Model.INN}";
        }
    }
}
