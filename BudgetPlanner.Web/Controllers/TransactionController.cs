﻿using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetTransactions(TransactionListRequestViewModel model)
        {
            var response = await MediatorService
                .Send(
                    new RetrieveTransactionsRequest { 
                        PageSize = model.PageSize,
                        PageNumber = model.PageNumber,
                        Reference = model.Reference, 
                        FromDate = model.FromDate, 
                        ToDate = model.ToDate 
                    });

            var viewModel = Map<RetrieveTransactionsResponse, TransactionListViewModel>(response);

            viewModel.SelectPageUrl = model.SelectPageUrl;
            viewModel.PreviousPageUrl = model.PreviousPageUrl;
            viewModel.NextPageUrl = model.NextPageUrl;

            return Json();
        }
    }
}
