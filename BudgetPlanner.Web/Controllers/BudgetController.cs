﻿using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> Details([FromRoute]string reference, [FromQuery]int pageSize=8, [FromQuery]int pageNumber=1)
        {
            var response = await MediatorService
                .Send(new RetrieveBudgetPlannerRequest { 
                    AccountId = (await CurrentAccount).Id, 
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
        public async Task<ActionResult> Create([FromQuery]bool isModal = false)
        {
            return await ViewWithContent(ContentConstants.BudgetPlannerEditor, 
                new CreateBudgetPlannerViewModel { Active = true, IsModal = isModal });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]CreateBudgetPlannerViewModel model)
        {
            if(!ModelState.IsValid)
                return await ViewWithContent(ContentConstants.BudgetPlannerEditor,
                    model);

            model.AccountId =  (await CurrentAccount).Id;

            var createBudgetPlannerRequest = Map<CreateBudgetPlannerViewModel,CreateBudgetPlannerRequest>(model);

            var saveResponse = await MediatorService
                .Send(createBudgetPlannerRequest);

            if(saveResponse.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = model.Reference });

            AddErrorsToModelState(saveResponse);

            return await ViewWithContent(ContentConstants.BudgetPlannerEditor, model);
        }

        [HttpGet, Route("/[controller]/Details/{reference}/Create")]
        public async Task<ActionResult> CreateTransaction([FromRoute]string reference, [FromQuery]bool isModal = false)
        {
            
            var budgetResponse = await MediatorService
                .Send(new RetrieveBudgetPlannerRequest { 
                    AccountId = (await CurrentAccount).Id, 
                    Reference = reference });

            if(budgetResponse.BudgetPlanner == null)
                return RedirectToAction("Index","Home");

            return await ViewWithContent(ContentConstants.TransactionEditor, new AddBudgetTransactionViewModel { 
                IsModal = isModal,
                BudgetId = budgetResponse.BudgetPlanner.Id,
                Active = true,
                TransactionTypes = await GetTransactionTypes() });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveTransaction(AddBudgetTransactionViewModel model)
        {
            model.TransactionTypes = await GetTransactionTypes();

            if (!ModelState.IsValid)
                return View("CreateTransaction", model);
            
            var createTransactionRequest = Map<AddBudgetTransactionViewModel, CreateTransactionRequest>(model);
            createTransactionRequest.AccountId = (await CurrentAccount).Id;
            var response = await MediatorService.Send(createTransactionRequest);
            
            if(response.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = response.Reference });

            return await ViewWithContent(ContentConstants.TransactionEditor, model);
        }

        private async Task<SelectList> GetTransactionTypes()
        {
            var response = await MediatorService
                .Send(new RetrieveTransactionTypesRequest());

            return new SelectList(response.TransactionTypes, nameof(TransactionType.Id), nameof(TransactionType.Name));
        }
    }
}
