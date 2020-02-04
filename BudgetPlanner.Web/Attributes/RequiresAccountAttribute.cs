using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var httpContext = context.HttpContext;
            var logger = httpContext.RequestServices
                .GetRequiredService<ILogger<RequiresAccountAttribute>>();
            
            try
            {
                if(!httpContext
                .Request.Cookies.TryGetValue(CookieKeyValue, out var cookieValue))
                    throw new UnauthorizedAccessException();

                var cookieValidationService = httpContext.RequestServices
                    .GetRequiredService<ICookieValidationService>();
                
                var applicationSettings = httpContext.RequestServices
                    .GetRequiredService<ApplicationSettings>();
                
                var account = await cookieValidationService.ValidateCookieToken(tokenValidation => { 
                    tokenValidation.ValidAudiences = applicationSettings.Audiences;
                    tokenValidation.ValidIssuers = applicationSettings.Issuers;
                }, cookieValue);

                if(!httpContext.Items.TryAdd(DataConstants.AccountItem, account))
                    logger.LogWarning("Skipping Item - An item with the key '{0}' already exists.", DataConstants.AccountItem);
            }   
            catch(UnauthorizedAccessException ex)
            {
                context.Result = new UnauthorizedResult();
                logger.LogError(ex.Message, ex);
            }
            
        }

        public RequiresAccountAttribute(string cookieKeyValue)
        {
            CookieKeyValue = cookieKeyValue;
        }
    }
}
