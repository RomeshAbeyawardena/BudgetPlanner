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
            var httpContext = context.HttpContext;
            try
            {
                if(!httpContext
                .Request.Cookies.TryGetValue(CookieKeyValue, out var cookieValue))
                    throw new UnauthorizedAccessException();

                var cookieValidationService = httpContext.RequestServices.GetRequiredService<ICookieValidationService>();
                
                var account = await cookieValidationService.ValidateCookieToken(cookieValue);

                httpContext.Items.Add("Account", account);
            }
            catch(UnauthorizedAccessException ex)
            {
                #if(DEBUG)
                    context.Result = new UnauthorizedObjectResult(ex);
                #else
                    context.Result = new UnauthorizedResult();
                #endif
            }
            
        }

        public RequiresAccountAttribute(string cookieKeyValue)
        {
            CookieKeyValue = cookieKeyValue;
        }
    }
}
