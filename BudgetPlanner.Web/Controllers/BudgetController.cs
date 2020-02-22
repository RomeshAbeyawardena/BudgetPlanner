using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Services;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainResponse = DNI.Shared.Domains.Response;

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
                return RedirectToAction("Index", "Home");

            var budgetStatsResponse = await MediatorService.Send(new BudgetPlannerStatsRequest
            {
                BudgetId = response.Result.Id,
                FromDate = DateTime.Now.AddDays(-7),
                ToDate = DateTime.Now
            });

            if(!budgetStatsResponse.IsSuccessful)
                return RedirectToAction("Index","Home");

            var budgetPlannerDetailsViewModel = Map<Budget, BudgetPlannerDetailsViewModel>(response.Result);

            budgetPlannerDetailsViewModel.PageSize = pageSize;
            budgetPlannerDetailsViewModel.PageNumber = pageNumber;
            budgetPlannerDetailsViewModel.FromDate = DateTime.Now.AddDays(-30);
            budgetPlannerDetailsViewModel.ToDate = DateTime.Now;
            budgetPlannerDetailsViewModel.Balance = response.Amount;
            budgetPlannerDetailsViewModel.BudgetStatisticsRequest = new BudgetStatisticRequestViewModel 
            { 
                AccountId = (await CurrentAccount).Id,
                FromDate = DateTime.Now.AddDays(-5),
                ToDate = DateTime.Now,
                BudgetId = response.Result.Id
            };

            return await ViewWithContent(ContentConstants.DetailsContentPath, budgetPlannerDetailsViewModel, 
                DictionaryBuilder.Create<string, string>(dictionaryBuilder => dictionaryBuilder
                    .Add("startDate", budgetPlannerDetailsViewModel.FromDate.ToString(FormatConstants.LongDateFormat))
                    .Add("endDate", budgetPlannerDetailsViewModel.ToDate.ToString(FormatConstants.LongDateFormat)))
                .ToDictionary());
        }

        [HttpGet]
        public async Task<string> CalculateNewBalance(int budgetId, int transactionTypeId, decimal amount)
        {
            var response = await MediatorService.Send(new RetrieveBudgetPlannerRequest
            {
                BudgetPlannerId = budgetId,
                AccountId = (await CurrentAccount).Id
            });

            var newBalance = response.Amount - amount;

            if(transactionTypeId == 1)
                newBalance = response.Amount + amount;

            return await GetContent(ContentConstants.TransactionEditorPath, 
                ContentConstants.EstimatedCostCalculatorLabel, 
                DictionaryBuilder.Create<string, string>(builder => builder
                    .Add("newBalance", newBalance.ToString(FormatConstants.CurrencyFormat)))
                .ToDictionary());
        }

        [HttpGet]
        public async Task<ActionResult> Edit([FromQuery]bool isModal)
        {
            return await ViewWithContent(ContentConstants.BudgetPlannerEditorPath, 
                new CreateBudgetPlannerViewModel { Active = true, IsModal = isModal });
        }

        [HttpGet]
        public async Task<ActionResult> Create([FromQuery]bool isModal = false)
        {
            return await ViewWithContent(ContentConstants.BudgetPlannerEditorPath, 
                new CreateBudgetPlannerViewModel { Active = true, IsModal = isModal });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Save([FromForm]CreateBudgetPlannerViewModel model)
        {
            if(!ModelState.IsValid)
                return await ViewWithContent(ContentConstants.BudgetPlannerEditorPath,
                    model);

            model.AccountId =  (await CurrentAccount).Id;

            var createBudgetPlannerRequest = Map<CreateBudgetPlannerViewModel,CreateBudgetPlannerRequest>(model);

            var saveResponse = await MediatorService
                .Send(createBudgetPlannerRequest);

            if(saveResponse.IsSuccessful)
                return RedirectToAction("Details", "Budget", new { reference = model.Reference });

            AddErrorsToModelState(saveResponse);

            return await ViewWithContent(ContentConstants.BudgetPlannerEditorPath, model);
        }

        [HttpGet, Route("/[controller]/Details/Edit/{id}")]
        public async Task<ActionResult> EditTransaction([FromRoute] int id, [FromQuery]bool isModal = false)
        {
            var response = await MediatorService
                .Send(new RetrieveTransactionRequest { 
                    AccountId = (await CurrentAccount).Id, 
                    TransactionId = id
                });

            if(!response.IsSuccessful || response.Result == null)
                return RedirectToAction("Index","Home");

            var viewModel = Map<Transaction, AddBudgetTransactionViewModel>(response.Result);

            viewModel.TransactionTypes = await GetTransactionTypes();
            viewModel.IsModal = isModal;

            return await ViewWithContent(ContentConstants.TransactionEditorPath, viewModel);
        }

        [HttpGet, Route("/[controller]/Details/{reference}/Create")]
        public async Task<ActionResult> CreateTransaction([FromRoute]string reference, [FromQuery]bool isModal = false)
        {
            
            var budgetResponse = await MediatorService
                .Send(new RetrieveBudgetPlannerRequest { 
                    AccountId = (await CurrentAccount).Id, 
                    Reference = reference });

            if(!DomainResponse.IsSuccessful(budgetResponse))
                return RedirectToAction("Index","Home");

            return await ViewWithContent(ContentConstants.TransactionEditorPath, new AddBudgetTransactionViewModel { 
                IsModal = isModal,
                BudgetId = budgetResponse.Result.Id,
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

            return await ViewWithContent(ContentConstants.TransactionEditorPath, model);
        }

        [HttpGet]
        public async Task<ActionResult> GetBudgetPlanners(BudgetPanelDashboardListViewModel model)
        {
            model.AccountId = (await CurrentAccount).Id;
            var retrieveBudgetPlannersRequest = Map<BudgetPanelDashboardListViewModel, RetrieveBudgetPlannersRequest>(model);
            
            var response = await MediatorService
                .Send(retrieveBudgetPlannersRequest);

            var budgetPlanners = Map<Domains.Dto.Budget, BudgetPanelDashboardItemViewModel>(response.Result);
            
            return Json(budgetPlanners);
        }

        private async Task<SelectList> GetTransactionTypes()
        {
            var response = await MediatorService
                .Send(new RetrieveTransactionTypesRequest());

            return new SelectList(response.Result, nameof(TransactionType.Id), nameof(TransactionType.Name));
        }
    }
}
