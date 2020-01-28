using BudgetPlanner.Domains.Data;
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

namespace BudgetPlanner.Web.Controllers
{
    public class BudgetController : DefaultControllerBase
    {
        [HttpGet, Route("[controller]/[action]/{reference}")]
        public async Task<ActionResult> Details([FromRoute]string reference)
        {
            var response = await MediatorService
                .Send<RetrieveBudgetPlannerResponse, RetrieveBudgetPlannerRequest>(new RetrieveBudgetPlannerRequest { Reference = reference });

            var budgetPlannerDetailsViewModel = Map<Budget, BudgetPlannerDetailsViewModel>(response.BudgetPlanner);
            return View(budgetPlannerDetailsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View(new CreateBudgetPlannerViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBudgetPlannerViewModel createBudgetPlannerViewModel)
        {
            var response = await MediatorService
                .Send<ValidateBudgetPlannerReferenceResponse, ValidateBudgetPlannerReferenceRequest>(
                    new ValidateBudgetPlannerReferenceRequest { UniqueReference = createBudgetPlannerViewModel.Reference } );
            return View();
        }
    }
}
