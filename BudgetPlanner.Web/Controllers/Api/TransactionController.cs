using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Services.Claims;
using BudgetPlanner.Domains.Requests;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class TransactionController : DefaultApiController
    {
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

            return Ok(response);
        }
    }
}
