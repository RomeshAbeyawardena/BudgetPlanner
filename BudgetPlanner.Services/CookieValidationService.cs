using BudgetPlanner.Contracts.Enumeration;
using DNI.Shared.Shared.Extensions;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Services.Claims;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Contracts.Services;
using DNI.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class CookieValidationService : ICookieValidationService
    {
        private readonly IAccountService _accountService;
        private readonly IClockProvider _clockProvider;
        private readonly ISwitch<string, EncryptionKey> _cryptographySwitch;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IJsonWebTokenService _jsonWebTokenService;

        public async Task<Account> ValidateCookieToken(Action<TokenValidationParameters> tokenValidationParameters, string cookieToken)
        {
            var defaultEncryptionKey = _cryptographySwitch.Case(EncryptionKeyConstants.Default);

            if (!_jsonWebTokenService.TryParseToken(cookieToken, defaultEncryptionKey.Password, tokenValidationParameters, Encoding.UTF8, out var claims))
                throw new UnauthorizedAccessException();

            var defaultClaim = claims.ToClaimObject<DefaultClaim>();

            var account = await _accountService.GetAccount(defaultClaim.AccountId, EntityUsage.UseLocally);

            if (account == null)
                throw new UnauthorizedAccessException();

            return await _encryptionProvider.Decrypt<Domains.Data.Account, Account>(account);
        }

        public async Task<string> CreateCookieToken(Action<SecurityTokenDescriptor> setupSecurityTokenDescriptor, Account account, int expiryPeriodInMinutes)
        {
            await Task.CompletedTask;
            var defaultEncryptionKey = _cryptographySwitch.Case(EncryptionKeyConstants.Default);
            return _jsonWebTokenService.CreateToken(setupSecurityTokenDescriptor,
                _clockProvider.UtcDateTime.AddMinutes(expiryPeriodInMinutes),
                DictionaryBuilder.Create<string, string>(builder => builder
                .Add(ClaimConstants.AccountIdClaim, account.Id.ToString())).ToDictionary(),
                defaultEncryptionKey.Password, Encoding.UTF8);
        }

        public void AppendSessionCookie(IResponseCookies responseCookies, string cookieName, string value, Action<CookieOptions> cookieOptionsAction = null)
        {
            CookieOptions cookieOptions = null;
            if (cookieOptionsAction != null)
            {
                cookieOptions = new CookieOptions();
                cookieOptionsAction(cookieOptions);
            }

            responseCookies.Append(cookieName, value, cookieOptions);
        }

        public void ConfigureCookieOptions(CookieOptions cookieOptions, int expiryPeriodInMinutes)
        {
            cookieOptions.Expires = _clockProvider.DateTimeOffset.AddMinutes(expiryPeriodInMinutes);
        }

        public CookieValidationService(IClockProvider clockProvider, ApplicationSettings applicationSettings,
            IEncryptionProvider encryptionProvider,
            IJsonWebTokenService jsonWebTokenService, IAccountService accountService)
        {
            _clockProvider = clockProvider;
            _cryptographySwitch = Switch.Create(applicationSettings.EncryptionKeys);
            _encryptionProvider = encryptionProvider;
            _jsonWebTokenService = jsonWebTokenService;
            _accountService = accountService;
        }
    }
}
