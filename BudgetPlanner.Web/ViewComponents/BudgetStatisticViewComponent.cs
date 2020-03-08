using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.ViewModels;
using DNI.Core.Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.ViewComponents
{
    public class BudgetStatisticsViewComponent : ViewComponentBase
    {
        public async Task<IViewComponentResult> InvokeAsync(BudgetStatisticRequestViewModel model)
        {
            var response = await MediatorService.Send(Map<BudgetStatisticRequestViewModel, BudgetPlannerStatsRequest>(model));

            if(!Response.IsSuccessful(response))
                throw new InvalidOperationException();

            var budgetPlannerStatsViewModel = new BudgetPlannerStatsViewModel { Statistics = response.Result };
            return await ViewWithContent(ContentConstants.BudgetStatisticsPanel, budgetPlannerStatsViewModel);
        }

    }
}
