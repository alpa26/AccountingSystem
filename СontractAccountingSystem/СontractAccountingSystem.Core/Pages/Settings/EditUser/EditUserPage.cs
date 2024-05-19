using Microsoft.EntityFrameworkCore.Metadata;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.EditUser
{
    public class EditUserPage : EditFormPage<UserModel>
    {
        [Required]
        public TextInput Login { get; } = new TextInput("Придумайте логин");
        [Required]
        public TextInput FirstName { get; } = new TextInput("Имя");
        [Required]
        public TextInput SecondName { get; } = new TextInput("Фамилия");
        [Required]
        public TextInput LastName { get; } = new TextInput("Отчество");
        [Required]
        public ComboBox<RoleEnum?> Role { get; } = new ComboBox<RoleEnum?>("Роль");
        [Required]
        public TextInput Email { get; } = new TextInput("Почта");

        public TextInput Phone { get; } = new TextInput("Телефон");


        public EditUserPage(UserModel model) : base(model)
        {
            CreateModelDelegate = CreateModel;

            if(Model.FullName is not null)
            {
                var name = Model.FullName.Split(' ');
                if (name.Length == 3)
                {
                    FirstName.Text = name[1];
                    SecondName.Text = name[0];
                    LastName.Text = name[2];
                }
            }

            Login.Text = Model.Login;
            Email.Text = Model.Email;
            Phone.Text = Model.Phone;

        }

        private UserModel CreateModel()
        {
            return new UserModel()
            {
                Id = new Guid()
            };
        }
    }
}
