using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.KontrAgentList;

namespace СontractAccountingSystem.Core.Pages.Settings
{
    public class KontrAgentTabsPage : TabSelectorPage
    {
        public Tab CreateTab { get; } = new Tab("Новый контрагент");
        public Tab ListTab { get; } = new Tab("Список");

        public KontrAgentTabsPage()
        {
            Tabs.AddRange(CreateTab, ListTab);

            CreateTab.CreateContentDelegate = () => new KontrAgentTypeListPage();
            ListTab.CreateContentDelegate = () => new KontrAgentListPage();
        }
    }
}
