using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using DNI.Core.Services;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.ViewComponents
{
    public class BudgetPlannerViewComponent : ViewComponentBase
    {
        public async Task<IViewComponentResult> InvokeAsync(object viewModel)
        {

            if(viewModel is BudgetPanelDashboardListViewModel budgetPanelDashboardListViewModel)
                return await List(budgetPanelDashboardListViewModel);

            if(viewModel is BudgetPanelDashboardItemViewModel budgetPanelDashboardItemViewModel)
                return View(budgetPanelDashboardItemViewModel);

            throw new NotSupportedException();
        }

        private async Task<IViewComponentResult> List(BudgetPanelDashboardListViewModel model)
        {
            var budgetPanelDashboardViewModel = new BudgetPanelDashboardViewModel();

            var retrieveBudgetPlannersRequest = Map<BudgetPanelDashboardListViewModel, RetrieveBudgetPlannersRequest>(model);

            var response = await MediatorService
                .Send(retrieveBudgetPlannersRequest);

            budgetPanelDashboardViewModel.BudgetPlanners = Map<Domains.Dto.Budget, BudgetPanelDashboardItemViewModel>(response.Result);

            return await ViewWithContent(
                (response.Result.Any()) 
                ? ContentConstants.Dashboard
                : ContentConstants.EmptyDashboard, "List", budgetPanelDashboardViewModel, DictionaryBuilder
                .Create<string, string>(dictionaryBuilder => dictionaryBuilder.Add("date", model.LastUpdated.ToString(FormatConstants.LongDateFormat)))
                .ToDictionary());
        }
    }
}
