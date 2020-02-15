using BudgetPlanner.Contracts.HttpServices;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.HttpServices
{
    public class CmsHttpService : HttpServiceBase, ICmsHttpService
    {
        private readonly HttpClient _cmsHttpService;

        public async Task<IDictionary<string, string>> GetContent(string path)
        {
            var response = (await _cmsHttpService
                .GetAsync(string.Format(ApiConstants.GetCmsContent, path)))
                .EnsureSuccessStatusCode();

            var dictionary = new Dictionary<string, string>();

            using (var doc = await GetJson(response.Content))
            {
                GetContent(dictionary, doc.RootElement);
            }
            return dictionary;

        }

        private void GetContent(IDictionary<string, string> state, JsonElement rootElement)
        {
            foreach (var item in rootElement.EnumerateObject())
            {
                if (item.Value.ValueKind == JsonValueKind.Object)
                {
                    GetContent(state, item.Value);
                }

                state.Add(item.Name, GetStringValue(item.Value));
            }
        }

        private string GetStringValue(JsonElement value)
        {
            var valueKind = value.ValueKind;
            if (valueKind == JsonValueKind.Array)
                return string.Empty;

            if (valueKind == JsonValueKind.False || valueKind == JsonValueKind.True)
                return value.GetBoolean().ToString();

            if (valueKind == JsonValueKind.Number)
                return value.GetDecimal().ToString();

            if (valueKind == JsonValueKind.String)
                return value.GetString();

            return default;
        }



        public CmsHttpService(ApplicationSettings applicationSettings, IHttpClientFactory httpClientFactory)
            : base(applicationSettings, httpClientFactory)
        {
            _cmsHttpService = GetHttpClient(HttpServiceConstants.ContentApiKey, configuration => { });
        }
    }
}
