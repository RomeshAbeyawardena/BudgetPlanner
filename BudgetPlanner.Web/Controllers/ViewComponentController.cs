using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.ViewComponents;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class ViewComponentController : DefaultControllerBase
    {
        public async Task<ActionResult> TransactionList([FromQuery] TransactionListRequestViewModel request)
        {
            await Task.CompletedTask;

            return ViewComponent(typeof(TransactionListViewComponent), new TransactionListRequestViewModel
            {
                BudgetId = request.BudgetId,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                PageSize = request.PageSize,
                PageNumber = request.PageNumber
            });
        }
    }
}
