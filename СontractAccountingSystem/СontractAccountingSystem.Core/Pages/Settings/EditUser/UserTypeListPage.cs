using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СontractAccountingSystem.Core.Pages.Settings.EditUser
{
    public class UserTypeListPage : ListPage<string>
    {
        public UserTypeListPage()
        {
            Title = "Добавить пользователя";
            DataSource.Fill("Новый пользователь");
            CreateItemPageDelegate = CreateEditPage;
        }

        private Page CreateEditPage(string selectedItem)
        {
            return new EditUserPage(null);
        }
    }
}
