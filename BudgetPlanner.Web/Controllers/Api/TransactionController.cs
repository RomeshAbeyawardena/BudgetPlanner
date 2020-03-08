using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Services.Claims;
using BudgetPlanner.Domains.Requests;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using BudgetPlanner.Domains.ViewModels;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class TransactionController : DefaultApiController
    {
        [HttpGet]
        public async Task<ActionResult> GetTransactions([Bind(Prefix = "payload")]string token)
        {
            var transactionClaim = GetClaim<TransactionClaim>(token);

            var response = await MediatorService
                .Send(
                    new RetrieveTransactionsRequest
                    {
                        PageSize = transactionClaim.PageSize,
                        PageNumber = transactionClaim.PageNumber,
                        Reference = transactionClaim.Reference,
                        FromDate = transactionClaim.FromDate,
                        ToDate = transactionClaim.ToDate
                    });

            return ResponseResult(response);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransaction([FromHeader, Bind(Prefix = "payload")]string token, 
            AddBudgetTransactionViewModel model)
        {
            var transactionClaim = GetClaim<TransactionClaim>(token);

            if(model.BudgetId != transactionClaim.BudgetPlannerId)
                throw new UnauthorizedAccessException();
            
            var request = Map<AddBudgetTransactionViewModel, CreateTransactionRequest>(model);

            request.AccountId = transactionClaim.AccountId;

            var response = await MediatorService.Send(request);
            
            return ResponseResult(response);
        }
    }
}
