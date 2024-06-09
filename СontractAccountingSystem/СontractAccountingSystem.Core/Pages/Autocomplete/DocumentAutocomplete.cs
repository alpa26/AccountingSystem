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
    public class DocumentAutocomplete : Autocomplete<RelateDocumentModel>
    {
        public string Type { get; set; }
        public DocumentAutocomplete(string type=null,string caption = null) : base(caption)
        {

            Type = type;
            RegisterBuildItemDelegate(x => new DocumentAutocompleteItem(x));
            AsyncLoadDelegate = Search;
            
        }

        private async Task<List<RelateDocumentModel>> Search(DataRequest request)
        {
            IList<RelateDocumentModel> relateDocuments;
            if(Type ==null)
                relateDocuments = await Service<IOrgStructureService>.GetInstance().LoadRelatedDocuments();
            else 
                relateDocuments = await Service<IOrgStructureService>.GetInstance().LoadRelatedDocumentsByType(Type);
            var pattern = request.SearchPattern?.ToLower();
            if (pattern.IsNullOrEmpty())
                return null;
            var result = relateDocuments
                .Where(x => x.DocumentName.ToLower().Contains(pattern) || x.DocumentNumber.ToLower().Contains(pattern))
                .ToList();
            return result;
        }
    }
}
