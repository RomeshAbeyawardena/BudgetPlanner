using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ICookieValidationService
    {
        IDictionary<string, string> ValidateToken(Action<TokenValidationParameters> tokenValidationParameters, string encryptionKey, string cookieToken);
        Task<Account> ValidateCookieToken(Action<TokenValidationParameters> tokenValidationParameters, string cookieToken, CancellationToken cancellationToken);
        Task<string> CreateCookieToken(Action<SecurityTokenDescriptor> setupSecurityTokenDescriptor, string encryptionKey, IDictionary<string, string> claims, int expiryPeriodInMinutes, CancellationToken cancellationToken);
        Task<string> CreateCookieToken(Action<SecurityTokenDescriptor> setupSecurityTokenDescriptor,
            Account account, int expiryPeriodInMinutes, CancellationToken cancellationToken);
        void AppendSessionCookie(IResponseCookies responseCookies, string cookieName, string value, CancellationToken cancellationToken, Action<CookieOptions> cookieOptionsBuilder = null);
        void ConfigureCookieOptions(CookieOptions cookieOptions, int expiryPeriodInMinutes, CancellationToken cancellationToken);
    }
}
