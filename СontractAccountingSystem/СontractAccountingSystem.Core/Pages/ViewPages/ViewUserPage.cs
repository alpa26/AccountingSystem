using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Pages.Autocomplete;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewUserPage : ViewFormPage<UserModel>
    {
        public TextField Login { get; } = new TextField("Логин");
        public TextField FullName { get; } = new TextField("ФИО");
        public TextField<RoleEnum> Role { get; } = new TextField<RoleEnum>("Роль");
        public TextField Email { get; } = new TextField("Почта");
        public TextField Phone { get; } = new TextField("Роль");
        public CollectionViewer<KontrAgentModel> KontrAgents { get; } = new CollectionViewer<KontrAgentModel>("Контрагенты");
        public CollectionViewer<OrganizationModel> Organizations { get; } = new CollectionViewer<OrganizationModel>("Организации");
        public CollectionViewer<RelateDocumentModel> RelateDocuments { get; } = new CollectionViewer<RelateDocumentModel>("Документы");
        public Button EditButton { get; } = new Button { Icon = IconType.Pencil, Hint = "Редактировать" };


        public ViewUserPage(UserModel model) : base(model)
        {
            Title = $"Пользователь {Model.Login}";
            HeaderActionPanel.Buttons.AddRange(EditButton);
        }


        protected override void Setup()
        {

            Login.Text = Model.Login;
            FullName.Text = Model.GetFullName();
            Email.Text = Model.Email;
            Phone.Text = Model.Phone;
            Role.Text = typeof(RoleEnum)
                                .GetField(Model.Role.ToString())
                                .GetCustomAttribute<DescriptionAttribute>()
                                ?.Description;

            KontrAgents.Items.Clear();
            KontrAgents.Items.AddRange(Model.KontrAgents);
            KontrAgents.RegisterBuildItemDelegate(x => new KontrAgentAutocompleteItem(x));
            KontrAgents.EmptyText = "Нет привязанных контрагентов";

            Organizations.Items.Clear();
            Organizations.Items.AddRange(Model.Organizations);
            Organizations.RegisterBuildItemDelegate(x => new OrganizationAutocompleteItem(x));
            Organizations.EmptyText = "Нет привязанных исполнителей";

            RelateDocuments.Items.Clear();
            RelateDocuments.Items.AddRange(Model.Documents);
            RelateDocuments.RegisterBuildItemDelegate(x => new DocumentAutocompleteItem(x));
            RelateDocuments.EmptyText = "Нет привязанных документов";
            RelateDocuments.CreateItemViewPageDelegate = x => new ViewDocumentPage(x.RelatedDocumentId);
        }
    }
}
