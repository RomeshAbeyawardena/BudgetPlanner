﻿using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
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
    public class BudgetController : ControllerBase
    {
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        [HttpGet, Route("/[controller]/[action]/{reference}")]
        [RequiresAccount(DataConstants.AccountSessionCookie)]
        public async Task<ActionResult> Details([FromRoute]string reference, [FromQuery]int pageSize=12, [FromQuery]int pageNumber=1)
        {
            var response = await MediatorService
                .Send(new RetrieveBudgetPlannerRequest { 
                    AccountId = CurrentAccount.Id, 
                    Reference = reference 
                });

            if(!response.IsSuccessful)
                return RedirectToAction("Index","Home");

            var budgetPlannerDetailsViewModel = Map<Budget, BudgetPlannerDetailsViewModel>(response.BudgetPlanner);

            budgetPlannerDetailsViewModel.PageSize = pageSize;
            budgetPlannerDetailsViewModel.PageNumber = pageNumber;
            budgetPlannerDetailsViewModel.FromDate = DateTime.Now.AddDays(-30);
            budgetPlannerDetailsViewModel.ToDate = DateTime.Now;
            budgetPlannerDetailsViewModel.Balance = response.Amount;

            return View(budgetPlannerDetailsViewModel);
        }

        [HttpGet]
        [RequiresAccount(DataConstants.AccountSessionCookie)]
        public async Task<ActionResult> Create([FromQuery]bool isModal = false)
        {
            await Task.CompletedTask;
            return View(new CreateBudgetPlannerViewModel { Active = true, IsModal = isModal });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [RequiresAccount(DataConstants.AccountSessionCookie)]
        public async Task<ActionResult> Create([FromForm]CreateBudgetPlannerViewModel createBudgetPlannerViewModel)
        {
            if(!ModelState.IsValid)
                return View(createBudgetPlannerViewModel);

            createBudgetPlannerViewModel.AccountId = CurrentAccount.Id;

            var createBudgetPlannerRequest = Map<CreateBudgetPlannerViewModel,CreateBudgetPlannerRequest>(createBudgetPlannerViewModel);

            var saveResponse = await MediatorService
                .Send(createBudgetPlannerRequest);

            if(saveResponse.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = createBudgetPlannerViewModel.Reference });

            AddErrorsToModelState(saveResponse);

            return View(createBudgetPlannerViewModel);
        }

        [HttpGet, Route("/[controller]/Details/{reference}/Create")]
        [RequiresAccount(DataConstants.AccountSessionCookie)]
        public async Task<ActionResult> CreateTransaction([FromRoute]string reference, [FromQuery]bool isModal = false)
        {
            
            var budgetResponse = await MediatorService
                .Send(new RetrieveBudgetPlannerRequest { 
                    AccountId = CurrentAccount.Id, 
                    Reference = reference });

            if(budgetResponse.BudgetPlanner == null)
                return RedirectToAction("Index","Home");

            return View(new AddBudgetTransactionViewModel { 
                IsModal = isModal,
                BudgetId = budgetResponse.BudgetPlanner.Id,
                Active = true,
                TransactionTypes = await GetTransactionTypes() });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [RequiresAccount(DataConstants.AccountSessionCookie)]
        public async Task<ActionResult> SaveTransaction(AddBudgetTransactionViewModel model)
        {
            model.TransactionTypes = await GetTransactionTypes();

            if (!ModelState.IsValid)
                return View("CreateTransaction", model);

            var createTransactionRequest = Map<AddBudgetTransactionViewModel, CreateTransactionRequest>(model);

            var response = await MediatorService.Send(createTransactionRequest);
            
            if(response.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = response.Reference });

            return View("CreateTransaction", model);
        }

        private async Task<SelectList> GetTransactionTypes()
        {
            var response = await MediatorService
                .Send(new RetrieveTransactionTypesRequest());

            return new SelectList(response.TransactionTypes, nameof(TransactionType.Id), nameof(TransactionType.Name));
        }
    }
}
