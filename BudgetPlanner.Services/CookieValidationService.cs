using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Contracts.Services;
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
        private readonly ISwitch<string, EncryptionKey> _cryptographySwitch;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IJsonWebTokenService _jsonWebTokenService;

        public async Task<Account> ValidateCookieToken(string cookieToken)
        {
                var defaultEncryptionKey = _cryptographySwitch.Case(EncryptionKeyConstants.Default);
                _jsonWebTokenService.TryParseToken(cookieToken, defaultEncryptionKey.Salt, parameters => { }, Encoding.UTF8, out var claims);

                if (!claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim) || int.TryParse(accountIdClaim, out var accountId))
                    throw new UnauthorizedAccessException();

                var account = await _accountService.GetAccount(accountId);

                if (account == null)
                    throw new UnauthorizedAccessException();

                return await _encryptionProvider.Decrypt<Domains.Data.Account, Account>(account);
        }

        public CookieValidationService(ISwitch<string, EncryptionKey> cryptographySwitch, 
            IEncryptionProvider encryptionProvider,
            IJsonWebTokenService jsonWebTokenService, IAccountService accountService)
        {
            _cryptographySwitch = cryptographySwitch;
            _encryptionProvider = encryptionProvider;
            _jsonWebTokenService = jsonWebTokenService;
            _accountService = accountService;
        }
    }
}
