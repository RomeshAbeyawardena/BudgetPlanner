using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class BudgetController : DefaultController
    {
        [HttpGet]
        public async Task<ActionResult> GetBudgetPlanners([Bind(Prefix = "payload")]string token)
        {
            var claims = GetTokenClaims(token);
            var request = new RetrieveBudgetPlannersRequest();

            if (claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim)
                && int.TryParse(accountIdClaim, out var accountId))
                request.AccountId = accountId;

            var response = await MediatorService.Send<RetrieveBudgetPlannersResponse>(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetBudgetPlanner([Bind(Prefix = "payload")]string token)
        {
            var claims = GetTokenClaims(token);

            var request = new RetrieveBudgetPlannerRequest();

            if(claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim) 
                && int.TryParse(accountIdClaim, out var accountId))
                request.AccountId = accountId;

            if (claims.TryGetValue(DataConstants.BudgetPlannerIdClaim, out var budgetPlannerIdClaim)
                && int.TryParse(budgetPlannerIdClaim, out var budgetPlannerId))
                request.BudgetPlannerId = budgetPlannerId;

            if (claims.TryGetValue(DataConstants.BudgetPlannerReferenceClaim, out var budgetPlannerReferenceClaim))
                request.Reference = budgetPlannerReferenceClaim;

            var response = await MediatorService.Send<RetrieveBudgetPlannerResponse>(request);

            return Ok(response);
        }

        [HttpPost]
        
        public async Task<ActionResult> CreateBudgetPlanner([Bind(Prefix = "payload")]string token, [FromForm] CreateBudgetPlannerViewModel model)
        {
            var claims = GetTokenClaims(token);
            int accountId = default;

            if (!claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim)
                && int.TryParse(accountIdClaim, out accountId))
                throw new UnauthorizedAccessException();

            if (model.AccountId != accountId)
                throw new UnauthorizedAccessException();

            var request = Map<CreateBudgetPlannerViewModel, CreateBudgetPlannerRequest>(model);

            await MediatorService.Send<CreateBudgetPlannerResponse>(request);

            return Ok();
        }
    }
}