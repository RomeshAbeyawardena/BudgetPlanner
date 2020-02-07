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

namespace BudgetPlanner.Web.ViewComponents
{
    public class BudgetPlannerViewComponent : DefaultViewComponentBase
    {
        public async Task<IViewComponentResult> InvokeAsync(object viewModel)
        {

            if(viewModel is BudgetPanelDashboardListViewModel budgetPanelDashboardListViewModel)
                return await List(budgetPanelDashboardListViewModel);

            if(viewModel is BudgetPanelDashboardItemViewModel budgetPanelDashboardItemViewModel)
                return View(budgetPanelDashboardItemViewModel);

            throw new NotSupportedException();
        }

        private async Task<IViewComponentResult> List(BudgetPanelDashboardListViewModel budgetPanelDashboardListViewModel)
        {
            var budgetPanelDashboardViewModel = new BudgetPanelDashboardViewModel();

            var retrieveBudgetPlannersRequest = Map<BudgetPanelDashboardListViewModel, RetrieveBudgetPlannersRequest>(budgetPanelDashboardListViewModel);

            var response = await MediatorService
                .Send(retrieveBudgetPlannersRequest);

            budgetPanelDashboardViewModel.BudgetPlanners = Map<Domains.Dto.Budget, BudgetPanelDashboardItemViewModel>(response.BudgetPlanners);

            return View("List", budgetPanelDashboardViewModel);
        }
    }
}
