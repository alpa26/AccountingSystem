using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Models;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Services
{
    internal class OrgStructureService : IOrgStructureService
    {
        internal static  List<PersonModel> PersonsList ;

        internal static List<ArchiveDocumentModel> DocumentList;

        internal static  List<KontrAgentModel> KontrAgentList;

        internal static List<OrganizationModel> OrganizationList;

        public OrgStructureService()
        {
            Setup();
        }

        public async void Setup()
        {
            await RefreshData();
        }

        public async Task RefreshData()
        {
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            PersonsList = await LoadModelList<PersonModel>(httpClient, "workers/list");
            KontrAgentList = await LoadModelList<KontrAgentModel>(httpClient, "kontragent/list");
            OrganizationList = await LoadModelList<OrganizationModel>(httpClient, "organizations/list");
            DocumentList = await LoadModelList<ArchiveDocumentModel>(httpClient, "documents/geteditlist");
        }


        private async Task<List<TModel>> LoadModelList<TModel>(HttpClient httpClient, string path)
        {
            var response = await httpClient.GetAsync($"api/{path}");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<IEnumerable<TModel>>();
                return res.ToList();
            }
            return null;
        }

        public async Task<IList<KontrAgentModel>> LoadKontrAgents()
        {
            await Task.Delay(500);
            return KontrAgentList.ToList();
        }

        public async Task<IList<ArchiveDocumentModel>> LoadDocuments()
        {
            await Task.Delay(500);
            return DocumentList.ToList();
        }

        public async Task<IList<PaymentTermModel>> LoadPayments()
        {
            var resList = new List<PaymentTermModel>();
            foreach (var itemDoc in DocumentList)
                resList.AddRange(itemDoc.PaymentTerms);
            await Task.Delay(200);
            return resList;
        }

        public async Task<IList<OrganizationModel>> LoadOrganizations()
        {
            await Task.Delay(500);
            return OrganizationList.ToList();
        }

        public async Task<IList<PersonModel>> LoadWorkers()
        {
            await Task.Delay(500);
            return PersonsList.ToList();
        }

        public async Task<IList<RelateDocumentModel>> LoadRelatedDocuments()
        {
            await Task.Delay(500);
            var newList = new List<RelateDocumentModel>();
            foreach (var document in DocumentList)
                   newList.Add(new RelateDocumentModel
                   {
                       Id = Guid.NewGuid(),
                       RelatedDocumentId = document.Id,
                       DocumentName = document.Name,
                       DocumentNumber = document.DocumentNumber
                   });
            return newList.ToList();
        }

        public async Task<IList<RelateDocumentModel>> LoadRelatedDocumentsByType(string doctype)
        {
            string restype = DocToRelatedDoc[doctype];
            await Task.Delay(500);
            var newList = new List<RelateDocumentModel>();
            foreach (var document in DocumentList)
            {
                if (document.DocumentType == restype)
                    newList.Add(new RelateDocumentModel
                    {
                        Id = Guid.NewGuid(),
                        RelatedDocumentId = document.Id,
                        DocumentName = document.Name,
                        DocumentNumber = document.DocumentNumber
                    });
            }   
            return newList.ToList();
        }

        private Dictionary<string, string> DocToRelatedDoc = new Dictionary<string, string>()
        {
            {"Договор на работы","Дополнительное соглашение к договору на раб."},
            {"Дополнительное соглашение к договору на раб.","Договор на работы"},

            {"Договор на фактические услуги","Дополнительное соглашение к договору на усл."},
            {"Дополнительное соглашение к договору на усл.","Договор на фактические услуги"},

            {"Лицензионный договор","Дополнительное соглашение к договору на лиц."},
            {"Дополнительное соглашение к договору на лиц.","Лицензионный договор"}
        };
    }
}
