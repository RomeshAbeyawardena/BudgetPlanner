using BudgetPlanner.Contracts.HttpServices;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.HttpServices
{
    public abstract class HttpServiceBase : IHttpService
    {
        protected readonly ApplicationSettings _applicationSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        protected HttpClient DefaultHttpClient { get; }

        protected HttpServiceBase(ApplicationSettings applicationSettings, IHttpClientFactory httpClientFactory)
        {
            _applicationSettings = applicationSettings;
            _httpClientFactory = httpClientFactory;
            DefaultHttpClient = GetHttpClient(HttpServiceConstants.DefaultApiKey, configure => { });
        }

        public async Task<string> GetRequestToken()
        {
           var response = await DefaultHttpClient.GetAsync(HttpServiceConstants.GetRequestToken);

            if(response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            return default;
        }

        protected HttpClient GetHttpClient(string name, Action<HttpRequestMessage> configuration)
        {
            var apiName = name;
            _applicationSettings.Apis.TryGetValue(apiName, out var api);
            return _httpClientFactory.GetHttpClient(apiName, api.Uri, configuration);
        }

        protected MultipartFormDataContent CreateForm(IDictionary<string, object> formData)
        {
            var httpContent = new MultipartFormDataContent();
            foreach (var (key, value) in formData)
            {
                var stringContent = new StringContent(value.ToString());
                httpContent.Add(stringContent, key);
            }

            return httpContent;
        }
    }
}
