using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

        internal static  List<KontrAgentModel> KontrAgents;

        internal static List<OrganizationModel> Organizations;

        public OrgStructureService()
        {
            Setup();
        }

        public async void Setup()
        {
            var httpClient = ((SingletonHttpClient)Service<IHttpClient>.GetInstance()).HostHttpClient;
            var response = await httpClient.GetAsync("api/users/employeelist");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<IEnumerable<PersonModel>>();
                Persons = res.ToList();
            }
            var response2 = await httpClient.GetAsync("api/kontragent/list");
            if (response2.IsSuccessStatusCode)
            {
                var res = await response2.Content.ReadAsAsync<IEnumerable<KontrAgentModel>>();
                KontrAgents = res.ToList();
            }
            var response3 = await httpClient.GetAsync("api/organizations/list");
            if (response3.IsSuccessStatusCode)
            {
                var res = await response3.Content.ReadAsAsync<IEnumerable<OrganizationModel>>();
                Organizations =  res.ToList();
            }
        }

        public async Task<IList<KontrAgentModel>> LoadKontrAgents()
        {
            await Task.Delay(500);
            return KontrAgents.ToList();
        }

        public async Task<IList<OrganizationModel>> LoadOrganizations()
        {
            await Task.Delay(500);
            return Organizations.ToList();
        }

        public async Task<IList<PersonModel>> LoadEmployee()
        {
            await Task.Delay(500);
            return Persons.ToList();
        }
    }
}
