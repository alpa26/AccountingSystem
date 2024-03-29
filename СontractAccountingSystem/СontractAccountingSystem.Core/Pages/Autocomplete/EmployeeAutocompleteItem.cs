﻿using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class EmployeeAutocompleteItem : Item<PersonModel>
    {
        public Label FullName { get; } = new Label();
        public Label Role { get; } = new Label { Style = TextStyle.LightDescription };

        public EmployeeAutocompleteItem(PersonModel model) : base(model)
        {
            Layout = new VerticalStack(FullName, Role);
        }

        protected override void Setup()
        {
            FullName.Text = Model.FullName;
            Role.Text = Model.Role;
        }
    }
}
