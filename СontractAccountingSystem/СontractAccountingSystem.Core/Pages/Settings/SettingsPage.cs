using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.OrganizationList;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.WorkerList;

namespace СontractAccountingSystem.Core.Pages.Settings
{
    public class SettingsPage : TabSelectorPage
    {
        public Tab AddElements { get; } = new Tab("Добавить");
        public Tab Organizations { get; } = new Tab("Организации");
        public Tab Workers { get; } = new Tab("Работники");

        public SettingsPage()
        {
            Tabs.AddRange(AddElements, Organizations, Workers);

            AddElements.CreateContentDelegate = () => new SettingsListPage();
            Organizations.CreateContentDelegate = () => new OrganizationListPage();
            Workers.CreateContentDelegate = () => new WorkerListPage();
        }
    }
}
