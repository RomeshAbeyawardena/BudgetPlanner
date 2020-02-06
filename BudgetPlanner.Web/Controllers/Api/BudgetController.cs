using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
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
        public async Task<ActionResult> GetBudgetPlanners([Bind(Prefix = "payload")]string token)
        {
            var claims = GetTokenClaims(token);
            var request = new RetrieveBudgetPlannersRequest();

            if (claims.TryGetValue(DataConstants.AccountIdClaim, out var accountIdClaim)
                && int.TryParse(accountIdClaim, out var accountId))
                request.AccountId = accountId;

            var response = await MediatorService.Send<RetrieveBudgetPlannersResponse, RetrieveBudgetPlannersRequest>(request);

            return Ok(response);
        }

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

            var response = await MediatorService.Send<RetrieveBudgetPlannerResponse, RetrieveBudgetPlannerRequest>(request);

            return Ok(response);
        }

        public BudgetController()
        {

        }
    }
}
