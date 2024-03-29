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
    public class KontrAgentAutocomplete : Autocomplete<KontrAgentModel>
    {
        public KontrAgentAutocomplete(string caption = null) : base(caption)
        {
            RegisterBuildItemDelegate(x => new KontrAgentAutocompleteItem(x));
            AsyncLoadDelegate = Search;
        }

        private async Task<List<KontrAgentModel>> Search(DataRequest request)
        {
            var persons = await Service<IOrgStructureService>.GetInstance().LoadKontrAgents();
            var pattern = request.SearchPattern?.ToLower();
            if (pattern.IsNullOrEmpty())
                return null;
            var result = persons
                .Where(x => x.FullName.ToLower().Contains(pattern) || x.INN.ToLower().Contains(pattern))
                .ToList();
            return result;
        }
    }
}
