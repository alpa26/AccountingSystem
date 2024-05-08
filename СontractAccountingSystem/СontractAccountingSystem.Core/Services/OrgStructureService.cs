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
        internal static  List<PersonModel> Persons ;

        internal static List<ArchiveDocumentModel> Documents;

        internal static  List<KontrAgentModel> KontrAgents;

        internal static List<OrganizationModel> Organizations;

        public OrgStructureService()
        {
            Setup();
        }

        public async void Setup()
        {
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            Persons = await LoadModelList<PersonModel>(httpClient, "users/workerlist");
            KontrAgents = await LoadModelList<KontrAgentModel>(httpClient, "kontragent/list");
            Organizations = await LoadModelList<OrganizationModel>(httpClient, "organizations/list");
            Documents = await LoadModelList<ArchiveDocumentModel>(httpClient, "documents/geteditlist");
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
            return KontrAgents.ToList();
        }

        public async Task<IList<ArchiveDocumentModel>> LoadDocuments()
        {
            await Task.Delay(500);
            return Documents.ToList();
        }

        

        public async Task<IList<OrganizationModel>> LoadOrganizations()
        {
            await Task.Delay(500);
            return Organizations.ToList();
        }

        public async Task<IList<PersonModel>> LoadWorkers()
        {
            await Task.Delay(500);
            return Persons.ToList();
        }

        public async Task<IList<RelateDocumentModel>> LoadRelatedDocumentsByType(string type)
        {
            await Task.Delay(500);
            var newList = new List<RelateDocumentModel>();
            foreach (var document in Documents)
                if (document.DocumentType == type)
                    newList.Add(new RelateDocumentModel
                    {
                        Id = Guid.NewGuid(),
                        RelatedDocumentId = document.Id,
                        DocumentName = document.Name,
                        DocumentNumber = document.DocumentNumber
                    });
            return newList.ToList();
        }
    }
}
