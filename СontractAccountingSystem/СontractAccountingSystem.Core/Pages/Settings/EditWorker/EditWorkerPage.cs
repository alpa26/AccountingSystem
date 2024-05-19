using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.EditWorker
{
    public class EditWorkerPage : EditFormPage<PersonModel>
    {
        [Required]
        public TextInput FirstName { get; } = new TextInput("Имя");
        [Required]
        public TextInput SecondName { get; } = new TextInput("Фамилия");
        [Required]
        public TextInput LastName { get; } = new TextInput("Отчество");
        [Required]
        public TextInput StaffPosition { get; } = new TextInput("Должность");

        public EditWorkerPage(PersonModel model = null) : base(model)
        {
            CreateModelDelegate = CreateModel;

            if(Model.FullName is not null)
            {
                var name = Model.FullName.Split(' ');
                if(name.Length==3)
                {
                    FirstName.Text = name[1];
                    SecondName.Text = name[0];
                    LastName.Text = name[2];
                }
            }
            StaffPosition.Text = Model.StaffPosition;
        }

        private PersonModel CreateModel()
        {
            return new PersonModel()
            {
                Id = new Guid()
            };
        }
    }
}
