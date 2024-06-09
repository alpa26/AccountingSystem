using Microsoft.EntityFrameworkCore.Metadata;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.Settings.EditUser
{
    public class EditUserPage : EditFormPage<UserModel>
    {
        internal bool IsNew { get; set; } = false;
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

        public MultiValueAutocomplete<KontrAgentModel> KontrAgents { get; } =
            new MultiValueAutocomplete<KontrAgentModel>("Контрагенты");
        public MultiValueAutocomplete<OrganizationModel> Organizations { get; } =
            new MultiValueAutocomplete<OrganizationModel>("Организации");
        public MultiValueAutocomplete<RelateDocumentModel> Documents { get; } = 
            new MultiValueAutocomplete<RelateDocumentModel>("Доп Документы");

        public EditUserPage(UserModel model) : base(model)
        {
            Title = "Новый пользователь";
            Subtitle = "Укажите данные пользователя и права доступа";
            CreateModelDelegate = CreateModel;

            KontrAgents.BuildAutocompleteDelegate = () => new KontrAgentAutocomplete() { Placeholder = "Введите контрагента" };
            Organizations.BuildAutocompleteDelegate = () => new OrganizationAutocomplete() { Placeholder = "Введите исполнителя" };
            Documents.BuildAutocompleteDelegate = () => new DocumentAutocomplete() { Placeholder = "Введите номер документа или тип" };

        }

        protected override void Setup()
        {
            Login.Text = Model.Login;
            FirstName.Text = Model.FirstName;
            SecondName.Text = Model.SecondName;
            LastName.Text = Model.LastName;

            Role.Value = Model.Role;
            Email.Text = Model.Email;
            Phone.Text = Model.Phone;
            if (Model.KontrAgents is not null && Model.KontrAgents.Count != 0)
                KontrAgents.Value = Model.KontrAgents.ToArray();
            if (Model.Organizations is not null && Model.Organizations.Count != 0)
                Organizations.Value = Model.Organizations.ToArray();
            if (Model.Documents is not null && Model.Documents.Count != 0)
                Documents.Value = Model.Documents.ToArray();
        }

        private UserModel CreateModel()
        {
            IsNew = true;
            return new UserModel()
            {
                Id = new Guid()
            };
        }
    }
}
