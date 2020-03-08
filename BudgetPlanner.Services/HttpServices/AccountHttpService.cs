using BudgetPlanner.Contracts.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Core.Contracts.Services;
using BudgetPlanner.Domains;
using System.Net.Http;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using DNI.Core.Services;
using DNI.Core.Shared.Extensions;

namespace BudgetPlanner.Services.HttpServices
{
    public class AccountHttpService : HttpServiceBase, IAccountHttpService
    {
        private ICookieValidationService _cookieValidationService;
        private readonly HttpClient _accountHttpClient;

        public AccountHttpService(ApplicationSettings applicationSettings, 
            IHttpClientFactory httpClientFactory,
            ICookieValidationService cookieValidationService) 
            : base(applicationSettings, httpClientFactory)
        {
            _cookieValidationService = cookieValidationService;
            _accountHttpClient = GetHttpClient(nameof(AccountHttpService), configure => { });
        }

        public async Task<LoginResponse> Login(string emailAddress, string password)
        {
            var requestToken = await GetRequestToken();

            var token = await _cookieValidationService.CreateCookieToken(configure => { }, EncryptionKeyConstants.Api, 
                DictionaryBuilder.Create<string, string>().Add(ClaimConstants.RequestTokenClaim, requestToken).ToDictionary(),
                _applicationSettings.SessionExpiryInMinutes);

            _accountHttpClient.DefaultRequestHeaders.Add("payload", token);

            var httpContent = CreateForm(DictionaryBuilder.Create<string, object>()
                .Add("emailAddress", emailAddress)
                .Add("password", emailAddress)
                .ToDictionary());

            var response = await _accountHttpClient.PostAsync(HttpServiceConstants.AuthenticateAccount, httpContent);
            if(response.IsSuccessStatusCode)
                return await response.Content.ToObject<LoginResponse>();

            return default;
        }
    }
}
