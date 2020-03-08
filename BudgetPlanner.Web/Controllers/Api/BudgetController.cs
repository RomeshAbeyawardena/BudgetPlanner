using BudgetPlanner.Services.Claims;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class BudgetController : DefaultApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetBudgetPlanners([Bind(Prefix = "payload")]string token)
        {
            var claimObject = GetClaim(token);

            var request = new RetrieveBudgetPlannersRequest {
                AccountId = claimObject.AccountId
            };
            
            var response = await MediatorService.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetBudgetPlanner([Bind(Prefix = "payload")]string token)
        {
            var budgetPlannerClaim = GetClaim<BudgetPlannerClaim>(token);

            var request = new RetrieveBudgetPlannerRequest
            {
                AccountId = budgetPlannerClaim.AccountId,
                BudgetPlannerId = budgetPlannerClaim.BudgetPlannerId,
                Reference = budgetPlannerClaim.Reference
            };

            var response = await MediatorService.Send(request);

            return ResponseResult(response);
        }

        

        [HttpPost]
        public async Task<ActionResult> CreateBudgetPlanner([Bind(Prefix = "payload")]string token, [FromForm] CreateBudgetPlannerViewModel model)
        {
            var budgetPlannerClaim = GetClaim<BudgetPlannerClaim>(token);

            if (model.AccountId != budgetPlannerClaim.AccountId)
                throw new UnauthorizedAccessException();

            var request = Map<CreateBudgetPlannerViewModel, CreateBudgetPlannerRequest>(model);

            var response = await MediatorService.Send(request);

            return ResponseResult(response);
        }
    }
}