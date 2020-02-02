using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Attributes
{
    public class RequiresAccountAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public string CookieKeyValue { get; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                if(!context.HttpContext
                .Request.Cookies.TryGetValue(CookieKeyValue, out var cookieValue))
                    throw new UnauthorizedAccessException();

                var accountService = context.HttpContext.RequestServices.GetRequiredService<IAccountService>();
                var jsonTokenService = context.HttpContext.RequestServices.GetRequiredService<IJsonWebTokenService>();
                var cryptographySwitch = context.HttpContext.RequestServices.GetRequiredService<ISwitch<string, EncryptionKey>>();

                var defaultEncryptionKey = cryptographySwitch.Case(EncryptionKeyConstants.Default);
                jsonTokenService.TryParseToken(cookieValue, defaultEncryptionKey.Salt, parameters => { }, Encoding.UTF8, out var claims);

                if (!claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim) || int.TryParse(accountIdClaim, out var accountId))
                    throw new UnauthorizedAccessException();

                var account = accountService.GetAccount(accountId);
                if (account == null)
                    throw new UnauthorizedAccessException();

                context.HttpContext.Items.Add("Account", account);
            }
            catch(UnauthorizedAccessException ex)
            {
                context.Result = new UnauthorizedObjectResult(ex);
            }
            
        }

        public RequiresAccountAttribute(string cookieKeyValue)
        {
            CookieKeyValue = cookieKeyValue;
        }
    }
}
