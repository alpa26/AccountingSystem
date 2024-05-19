using Microsoft.EntityFrameworkCore.Metadata;
using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.EditKontrAgent
{
    public class EditKontrAgentPage : EditFormPage<KontrAgentModel>
    {
        internal string Type { get; set; }


        [Required]
        public TextInput FullName { get; } = new TextInput("Наименование");
        [Required]
        public TextInput INN { get; } = new TextInput("ИНН") { MaxLength = 10 };

        public TextInput KPP { get; } = new TextInput("КПП") { MaxLength = 9 };
        [Required]
        public TextInput OGRN { get; } = new TextInput("Регистрационный номер") {MaxLength = 13};
        public TextInput ContactPersonName { get; } = new TextInput("Контактное лицо") { Placeholder = "ФИО" };
        [Required]
        public TextInput ContactPhone { get; } = new TextInput("Контактный телефон");
        public TextInput ContactEmail { get; } = new TextInput("Контактная почта");
        [Required]
        public TextInput Address { get; } = new TextInput("Физический адрес");

        public EditKontrAgentPage(string type) : this(null, type)
        {

        }

        public EditKontrAgentPage(KontrAgentModel model, string type) : base(model)
        {
            CreateModelDelegate = CreateModel;
            DeleteButton.Hidden = true;
            Type = type;
            Title = type;
        }

        protected override void Setup()
        {
            

            Content.Clear();
            if (Type == "Юридическое лицо")
            {
                Content.AddRange(
                FullName,
                INN,KPP, OGRN, 
                ContactPersonName, ContactPhone, ContactEmail,
                Address
                );
            }
            else
            {
                FullName.Placeholder = "ФИО";
                INN.MaxLength = 12;
                OGRN.MaxLength = 15;

                Content.AddRange(
                FullName,
                INN, OGRN,
                ContactPhone, ContactEmail,
                Address
                );
            }

            FullName.Text = Model.FullName;
            INN.Text = Model.INN;
            KPP.Text = Model.KPP;
            OGRN.Text = Model.OGRN;

            ContactPersonName.Text = Model.ContactPersonName;
            ContactPhone.Text = Model.ContactPhone;
            ContactEmail.Text = Model.ContactEmail;
            Address.Text = Model.Address;


        }

        private KontrAgentModel CreateModel()
        {
            return new KontrAgentModel()
            {
                Id = new Guid()
            };
        }
    }
}
