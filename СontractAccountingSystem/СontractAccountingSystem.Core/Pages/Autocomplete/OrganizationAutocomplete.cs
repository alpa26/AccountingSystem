using Salazki.Presentation.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Pages.Autocomplete
{
    public class OrganizationAutocomplete : Autocomplete<OrganizationModel>
    {
        public OrganizationAutocomplete(string caption = null) : base(caption)
        {
            RegisterBuildItemDelegate(x => new OrganizationAutocompleteItem(x));
            AsyncLoadDelegate = Search;
        }

        private async Task<List<OrganizationModel>> Search(DataRequest request)
        {
            var persons = await Service<IOrgStructureService>.GetInstance().LoadOrganizations();
            var pattern = request.SearchPattern?.ToLower();
            if (pattern.IsNullOrEmpty())
                return null;
            var result = persons
                .Where(x => x.Name.ToLower().Contains(pattern))
                .ToList();
            return result;
        }
    }
}
