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
                foreach (var item in doc.RootElement.EnumerateObject())
                {
                    dictionary.Add(item.Name, item.Value.GetRawText());
                }
            }
            return dictionary;
            
        }

        public CmsHttpService(ApplicationSettings applicationSettings, IHttpClientFactory httpClientFactory)
            : base(applicationSettings, httpClientFactory)
        {
            _cmsHttpService = GetHttpClient(nameof(CmsHttpService), configuration => { });
        }
    }
}
