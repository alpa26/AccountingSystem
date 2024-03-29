﻿using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class EmployeeAutocomplete : Autocomplete<PersonModel>
    {
        public EmployeeAutocomplete(string caption = null) : base(caption)
        {
            RegisterBuildItemDelegate(x => new EmployeeAutocompleteItem(x));
            AsyncLoadDelegate = Search;
            CanSelectValueDelegate = CanSelectValue;
        }

        private bool CanSelectValue(PersonModel value, out string message)
        {
            message = null;
            if (value.FullName.StartsWith("Перов"))
            {
                message = "Данный сотрудник временно отсутсвует";
                return false;
            }
            return true;
        }

        private async Task<List<PersonModel>> Search(DataRequest request)
        {
            var persons = await Service<IOrgStructureService>.GetInstance().LoadEmployee();
            var pattern = request.SearchPattern?.ToLower();
            if (pattern.IsNullOrEmpty())
                return null;
            var result = persons
                .Where(x => x.FullName.ToLower().Contains(pattern) || x.Role.ToLower().Contains(pattern))
                .ToList();
            return result;
        }
    }
}
