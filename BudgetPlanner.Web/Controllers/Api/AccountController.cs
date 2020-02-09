using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Services.Claims;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class AccountController : DefaultApiController
    {
        [HttpPost]
        public async Task<ActionResult> RegisterAccount([FromHeader, Bind(Prefix = "payload")]string token, [FromForm]RegisterAccountViewModel model)
        {
            var accountRegistrationClaim = GetClaim<RequestTokenClaim>(token);

            var validateClaimResponse = await MediatorService.Send(new ValidateTokenRequest { Token  = accountRegistrationClaim.Token });

            if(!validateClaimResponse.IsSuccessful)
                return ResponseResult(validateClaimResponse);

            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);

            var response = await MediatorService
                .Send(new RegisterAccountRequest { Account = mappedAccount });

            return ResponseResult(response);
        }

        [HttpPost]
        public async Task<ActionResult> AuthenticateAccount([FromHeader, Bind(Prefix = "payload")]string token, [FromForm]LoginViewModel model)
        {
            var accountRegistrationClaim = GetClaim<RequestTokenClaim>(token);

            var validateClaimResponse = await MediatorService.Send(new ValidateTokenRequest { Token  = accountRegistrationClaim.Token });

            if(!validateClaimResponse.IsSuccessful)
                return ResponseResult(validateClaimResponse);

            var response = await MediatorService
                .Send(new LoginRequest { 
                    EmailAddress = model.EmailAddress, 
                    Password = model.Password 
                });
            
            return ResponseResult(response);
        }

        [HttpGet]
        public async Task<ActionResult> GenerateRequestToken()
        {
            var response = await MediatorService.Send(new CreateTokenRequest { ValidityPeriodInMinutes = 15 });

            return ResponseResult(response);
        }
    }
}
