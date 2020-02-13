using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using BudgetPlanner.Web.ViewComponents;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class ViewComponentController : ControllerBase
    {
        [DisallowUntrustedReferrers("Details", "Budget")]
        public async Task<ActionResult> TransactionList([FromQuery] TransactionListRequestViewModel request)
        {
            
            await Task.CompletedTask;
            request.AccountId = (await CurrentAccount).Id;
            return ViewComponent(typeof(TransactionListViewComponent), request);
        }
        
        [DisallowUntrustedReferrers("Index", "Home")]
        public async Task<ActionResult> BudgetPlannerListDashboard(BudgetPanelDashboardListViewModel request)
        {
            await Task.CompletedTask;
            request.AccountId = (await CurrentAccount).Id;
            return ViewComponent(typeof(BudgetPlannerViewComponent), request);
        }
    }
}
