using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Services.Claims;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;
using DNI.Core.Contracts.Services;
using DNI.Core.Domains;
using DNI.Core.Services;
using DNI.Core.Services.Abstraction;
using DNI.Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlanner.Web.Controllers.Api
{
    [Route("/api/{controller}/{action}")]
    [ApiController, DNI.Core.Services.Attributes.HandleException]
    public abstract class DefaultApiController : DefaultApiControllerBase
    {
        protected ApplicationSettings ApplicationSettings => GetService<ApplicationSettings>();
        protected ICookieValidationService CookieValidationService => GetService<ICookieValidationService>();
        protected IClockProvider ClockProvider => GetService<IClockProvider>();
        protected IDictionary<string, string> GetTokenClaims(string token)
        {
            
            var tokenClaims = CookieValidationService.ValidateToken(tokenValidation => {
                tokenValidation.ValidAudiences = ApplicationSettings.Audiences;
                tokenValidation.ValidIssuers = ApplicationSettings.Issuers;
            }, EncryptionKeyConstants.Api, token);

            if(tokenClaims == null)
                throw new UnauthorizedAccessException();

            return tokenClaims;
        }

        protected async Task<string> GenerateToken(string issuer, string audience, IDictionary<string, string> claims, CancellationToken cancellationToken)
        {
            return await CookieValidationService.CreateCookieToken(configure => { 
                configure.Issuer = issuer; 
                configure.Audience = audience; 
            }, 
                EncryptionKeyConstants.Api, claims, 
                ApplicationSettings.SessionExpiryInMinutes, cancellationToken);
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

        protected string GetPayLoad(string payLoadKey)
        {
            if(!HttpContext.Items.TryGetValue(payLoadKey, out var token))
                throw new UnauthorizedAccessException();

            return token.ToString();
        }

    }
}
