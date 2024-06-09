using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;
using СontractAccountingSystem.Core.Pages.ViewPages;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.UserList
{
    public class UserListPage : ListPage<UserModel>
    {
        public UserListPage()
        {
            Title = "Пользователи";

            ListEmptyText = "Пользователей нет";

            RegisterBuildItemDelegate(x => new UserListItem(x));
            CreateItemPageDelegate = x => new ViewUserPage(x);
            AutoSelectFirstItem = true;
        }
    }
}
