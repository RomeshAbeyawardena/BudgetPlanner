using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class BudgetController : DefaultControllerBase
    {
        [HttpGet, Route("/[controller]/[action]/{reference}")]
        public async Task<ActionResult> Details([FromRoute]string reference)
        {
            var response = await MediatorService
                .Send<RetrieveBudgetPlannerResponse, RetrieveBudgetPlannerRequest>(new RetrieveBudgetPlannerRequest { Reference = reference });

            if(response.BudgetPlanner == null)
                return RedirectToAction("Index","Home");

            var budgetPlannerDetailsViewModel = Map<Budget, BudgetPlannerDetailsViewModel>(response.BudgetPlanner);

            budgetPlannerDetailsViewModel.FromDate = DateTime.Now.AddDays(-30);
            budgetPlannerDetailsViewModel.ToDate = DateTime.Now;

            return View(budgetPlannerDetailsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create([FromQuery]bool isModal = false)
        {
            await Task.CompletedTask;
            return View(new CreateBudgetPlannerViewModel { Active = true, IsModal = isModal });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBudgetPlannerViewModel createBudgetPlannerViewModel)
        {
            if(!ModelState.IsValid)
                return View(createBudgetPlannerViewModel);

            var response = await MediatorService
                .Send<ValidateBudgetPlannerReferenceResponse, ValidateBudgetPlannerReferenceRequest>(
                    new ValidateBudgetPlannerReferenceRequest { UniqueReference = createBudgetPlannerViewModel.Reference } );

            if (!response.IsUnique) 
            { 
                ModelState.AddModelError(nameof(createBudgetPlannerViewModel.Reference), "Unique Reference is not valid");

                return View(createBudgetPlannerViewModel);
            }

            var createBudgetPlannerRequest = Map<CreateBudgetPlannerViewModel,CreateBudgetPlannerRequest>(createBudgetPlannerViewModel);

            var saveResponse = await MediatorService
                .Send<CreateBudgetPlannerResponse, CreateBudgetPlannerRequest>(createBudgetPlannerRequest);

            if(saveResponse.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = createBudgetPlannerViewModel.Reference });

            return View(createBudgetPlannerViewModel);
        }

        [HttpGet, Route("/[controller]/Details/{reference}/Create")]
        public async Task<ActionResult> CreateTransaction([FromRoute]string reference, [FromQuery]bool isModal = false)
        {
            
            var budgetResponse = await MediatorService
                .Send<RetrieveBudgetPlannerResponse, RetrieveBudgetPlannerRequest>(new RetrieveBudgetPlannerRequest { Reference = reference });

            if(budgetResponse.BudgetPlanner == null)
                return RedirectToAction("Index","Home");


            return View(new AddBudgetTransactionViewModel { 
                IsModal = isModal,
                BudgetId = budgetResponse.BudgetPlanner.Id,
                Active = true,
                TransactionTypes = await GetTransactionTypes() });
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransaction(AddBudgetTransactionViewModel model)
        {
            model.TransactionTypes = await GetTransactionTypes();

            if (!ModelState.IsValid)
                return View("CreateTransaction", model);

            var createTransactionRequest = Map<AddBudgetTransactionViewModel, CreateTransactionRequest>(model);

            var response = await MediatorService.Send<CreateTransactionResponse,CreateTransactionRequest>(createTransactionRequest);
            
            if(response.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = response.Reference });

            return View("CreateTransaction", model);
        }

        private async Task<SelectList> GetTransactionTypes()
        {
            var response = await MediatorService
                .Send<RetrieveTransactionTypesResponse,RetrieveTransactionTypesRequest>(new RetrieveTransactionTypesRequest());

            return new SelectList(response.TransactionTypes, nameof(TransactionType.Id), nameof(TransactionType.Name));
        }
    }
}
