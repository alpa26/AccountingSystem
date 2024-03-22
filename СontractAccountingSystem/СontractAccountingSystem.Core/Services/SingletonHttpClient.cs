using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using СontractAccountingSystem.Core.Services.Interfaces;

namespace СontractAccountingSystem.Core.Services
{
    public class SingletonHttpClient : IHttpClient
    {
        private HttpClient _httpClient;
        public HttpClient HostHttpClient => _httpClient ??= Service<IHttpClientFactory>.GetInstance().CreateClient("Host");

    }
}
