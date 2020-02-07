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
        public async Task<IViewComponentResult> InvokeAsync(TransactionListRequestViewModel model)
        {
            var response = await MediatorService
                .Send<RetrieveTransactionsResponse>(
                    new RetrieveTransactionsRequest { 
                        PageSize = model.PageSize,
                        PageNumber = model.PageNumber,
                        Reference = model.Reference, 
                        FromDate = model.FromDate, 
                        ToDate = model.ToDate 
                    });

            return View(Map<RetrieveTransactionsResponse, TransactionListViewModel>(response));
        }
    }
}
