using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RetrieveTransactions : IRequestHandler<RetrieveTransactionsRequest, RetrieveTransactionsResponse>
    {
        private readonly ITransactionService _transactionService;

        public async Task<RetrieveTransactionsResponse> Handle(RetrieveTransactionsRequest request, CancellationToken cancellationToken)
        {
            return new RetrieveTransactionsResponse { Transactions = await _transactionService.GetTransactions(request.BudgetId, request.FromDate, request.ToDate) };
        }

        public RetrieveTransactions(ITransactionService transactionService)
        {
            _transactionService =  transactionService;
        }
    }
}
