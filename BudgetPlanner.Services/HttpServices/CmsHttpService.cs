using BudgetPlanner.Contracts.HttpServices;
using BudgetPlanner.Domains;
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
            _cmsHttpService.GetAsync(ApiConstants.GetCmsContent);
        }

        public CmsHttpService(ApplicationSettings applicationSettings, IHttpClientFactory httpClientFactory)
            : base(applicationSettings, httpClientFactory)
        {
            _cmsHttpService = GetHttpClient(nameof(CmsHttpService), configuration => { });
        }
    }
}
