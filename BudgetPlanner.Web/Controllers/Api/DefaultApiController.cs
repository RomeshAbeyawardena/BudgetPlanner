using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Services.Claims;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Contracts.Services;
using DNI.Shared.Domains;
using DNI.Shared.Services;
using DNI.Shared.Services.Abstraction;
using DNI.Shared.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlanner.Web.Controllers.Api
{
    [Route("/api/{controller}/{action}")]
    [ApiController]
    public abstract class DefaultApiController : DefaultApiControllerBase
    {
        protected ApplicationSettings ApplicationSettings => GetService<ApplicationSettings>();
        protected ICookieValidationService CookieValidationService => GetService<ICookieValidationService>();
        protected IClockProvider ClockProvider => GetService<IClockProvider>();
        protected IDictionary<string, string> GetTokenClaims(string token)
        {
            
            var tokenClaims = CookieValidationService.ValidateCookieToken(tokenValidation => {
                tokenValidation.ValidAudiences = ApplicationSettings.Audiences;
                tokenValidation.ValidIssuers = ApplicationSettings.Issuers;
            }, EncryptionKeyConstants.Api, token);

            if(tokenClaims == null)
                throw new UnauthorizedAccessException();

            return tokenClaims;
        }

        protected async Task<string> GenerateToken(string issuerAudience, IDictionary<string, string> claims)
        {
            return await CookieValidationService.CreateCookieToken(configure => { 
                configure.Issuer = issuerAudience; configure.Audience = issuerAudience; }, 
                EncryptionKeyConstants.Api, claims, 
                ApplicationSettings.SessionExpiryInMinutes);
        }

        protected TClaim GetClaim<TClaim>(string token)
        {
            var tokenClaims = GetTokenClaims(token);

            return tokenClaims.ToClaimObject<TClaim>();
        }
        protected DefaultClaim GetClaim(string token)
        {
            return GetClaim<DefaultClaim>(token);
        }

    }
}
