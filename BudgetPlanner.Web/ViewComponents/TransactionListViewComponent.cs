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
    public class TransactionListViewComponent : DefaultViewComponentBase
    {
        public async Task<IViewComponentResult> InvokeAsync(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var response = await MediatorService
                .Send<RetrieveTransactionsResponse, RetrieveTransactionsRequest>(
                    new RetrieveTransactionsRequest { 
                        BudgetId = budgetId, 
                        FromDate = fromDate, 
                        ToDate = toDate });

            return View(Map<RetrieveTransactionsResponse, TransactionListViewModel>(response));
        }
    }
}
