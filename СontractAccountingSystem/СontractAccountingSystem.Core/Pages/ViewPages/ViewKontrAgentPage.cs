using Salazki.Presentation.Elements;
using System.ComponentModel;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.ViewPages
{
    public class ViewKontrAgentPage : ViewFormPage<KontrAgentModel>
    {
        public TextField Type { get; } = new TextField("Тип");
        public TextField INN { get; } = new TextField("ИНН");
        public TextField KPP { get; } = new TextField("КПП");
        public TextField OGRN { get; } = new TextField("ОГРН");
        public TextField ContactPersonName { get; } = new TextField("Контактное лицо");
        public TextField ContactPhone { get; } = new TextField("Контактный телефон");
        public TextField ContactEmail { get; } = new TextField("Контактная почта");
        public TextField Address { get; } = new TextField("Адрес");

        public Button EditButton { get; } = new Button { Icon = IconType.Pencil, Hint = "Редактировать" };
        public Button DeleteButton { get; } = new Button("Удалить", ButtonStyle.DangerFilled);


        public ViewKontrAgentPage(KontrAgentModel model) : base(model)
        {
            Title = Model.FullName;
            HeaderActionPanel.Buttons.AddRange(EditButton, DeleteButton);
        }


        protected override void Setup()
        {
            Title = Model.FullName;
            Content.Clear();
            if (Model.Type == "Юридическое лицо")
            {
                Content.AddRange(
                INN, KPP, OGRN,
                ContactPersonName, ContactPhone, ContactEmail,
                Address
                );
            }
            else
            {
                OGRN.Caption = "ОГРНИП";
                Content.AddRange(
                INN, OGRN,
                ContactPhone, ContactEmail,
                Address
                );
            }
            INN.Text = Model.INN;
            KPP.Text = Model.KPP;
            OGRN.Text = Model.OGRN;

            ContactPersonName.Text = Model.ContactPersonName;
            ContactPhone.Text = Model.ContactPhone.IsNullOrEmpty() ? "Не указан" : Model.ContactPhone;
            ContactEmail.Text = Model.ContactEmail.IsNullOrEmpty() ? "Не указан" : Model.ContactEmail;
            Address.Text = Model.Address;
        }
    }
}
