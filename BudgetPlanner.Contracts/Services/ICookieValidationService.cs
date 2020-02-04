using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ICookieValidationService
    {
        Task<Account> ValidateCookieToken(string cookieToken);
        Task<string> CreateCookieToken(Action<SecurityTokenDescriptor> setupSecurityTokenDescriptor,
            Account account, int expiryPeriodInMinutes);
        void AppendSessionCookie(IResponseCookies responseCookies, string cookieName, string value, Action<CookieOptions> cookieOptionsBuilder = null);
        void ConfigureCookieOptions(CookieOptions cookieOptions, int expiryPeriodInMinutes);
    }
}
