using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Pages.Settings.EditUser;
using СontractAccountingSystem.Core.Pages.Settings.ListPages.UserList;

namespace СontractAccountingSystem.Core.Pages.Settings
{
    public class UserSettingPage : TabSelectorPage
    {
        public Tab AddElements { get; } = new Tab("Добавить");
        public Tab Users { get; } = new Tab("Пользователи");

        public UserSettingPage()
        {
            Tabs.AddRange(AddElements, Users);

            AddElements.CreateContentDelegate = () => new UserTypeListPage();
            Users.CreateContentDelegate = () => new UserListPage();
        }
    }
}
