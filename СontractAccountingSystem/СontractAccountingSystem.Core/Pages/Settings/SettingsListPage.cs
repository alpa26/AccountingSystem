using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Settings.EditOrganization;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;
using СontractAccountingSystem.Core.Pages.Settings.EditWorker;

namespace СontractAccountingSystem.Core.Pages.Settings
{
    public class SettingsListPage : ListPage<string>
    {
        public SettingsListPage()
        {
            Title = "Добавить";
            DataSource.Fill("Работник", "Организация");
            CreateItemPageDelegate = CreateEditPage;
        }

        private Page CreateEditPage(string selectedItem)
        {
            if (selectedItem == "Организация")
                return new EditOrganizationPage();
            else return new EditWorkerPage();
        }
    }
}
