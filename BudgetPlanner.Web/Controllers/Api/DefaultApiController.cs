﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public abstract class DefaultApiController : DefaultApiControllerBase
    {
        protected ApplicationSettings ApplicationSettings => GetService<ApplicationSettings>();
        protected ISwitch<string, EncryptionKey> CryptographySwitch => Switch.Create(ApplicationSettings.EncryptionKeys);
        protected IJsonWebTokenService JsonWebTokenService => GetService<IJsonWebTokenService>();
        protected IClockProvider ClockProvider => GetService<IClockProvider>();
        protected IDictionary<string, string> GetTokenClaims(string token)
        {
            var defaultEncryptionKey = CryptographySwitch.Case(EncryptionKeyConstants.Api);
            if(!JsonWebTokenService.TryParseToken(token, defaultEncryptionKey.Password, tokenValidation => {
                tokenValidation.ValidAudiences = ApplicationSettings.Audiences;
                tokenValidation.ValidIssuers = ApplicationSettings.Issuers;
            }, Encoding.UTF8, out var tokenClaims))
                throw new UnauthorizedAccessException();

            return tokenClaims;
        }

        protected string GenerateToken(string issuerAudience, string secret,  IDictionary<string, string> claims)
        {
            return JsonWebTokenService.CreateToken(configure => { configure.Issuer = issuerAudience; configure.Audience = issuerAudience; }, 
                ClockProvider.UtcDateTime.AddMinutes(ApplicationSettings.SessionExpiryInMinutes), 
                claims, secret, Encoding.UTF8);
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
