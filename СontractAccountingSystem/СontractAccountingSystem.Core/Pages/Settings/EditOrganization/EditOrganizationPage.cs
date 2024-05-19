using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Settings.EditOrganization
{
    public class EditOrganizationPage : EditFormPage<OrganizationModel>
    {
        [Required]
        public TextInput Name { get; } = new TextInput("Название");

        public EditOrganizationPage(OrganizationModel model=null) : base(model)
        {
            CreateModelDelegate = CreateModel;
            Name.Text = Model.Name;
        }

        private OrganizationModel CreateModel()
        {
            return new OrganizationModel()
            {
                Id = new Guid()
            };
        }
    }
}
