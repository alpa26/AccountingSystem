using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.ListPages.UserList
{
    public class UserListItem : Item<UserModel>
    {
        public Label FullName { get; } = new Label();
        public Label Role { get; } = new Label { Style = TextStyle.LightDescription };

        public UserListItem(UserModel model) : base(model)
        {
            Layout = new VerticalStack(FullName, Role);
        }

        protected override void Setup()
        {
            FullName.Text = Model.GetFullName();
            Role.Text = typeof(RoleEnum)
                                .GetField(Model.Role.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;
        }
    }
}
