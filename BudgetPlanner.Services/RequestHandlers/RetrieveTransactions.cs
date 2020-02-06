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
            var transactionsPager = _transactionService
                .GetPagedTransactionsWithLedgers(request.Reference, request.FromDate, request.ToDate);

            return new RetrieveTransactionsResponse { 
                PageNumber = request.PageNumber,
                TotalPages = await transactionsPager.GetTotalNumberOfPages(request.PageSize),
                Transactions = await transactionsPager
                    .GetPagedItems(builder => { 
                        builder.PageNumber = request.PageNumber;
                        builder.MaximumRowsPerPage = request.PageSize; 
                        builder.UseAsync = true;
                    }, cancellationToken: cancellationToken) };
        }

        public RetrieveTransactions(ITransactionService transactionService)
        {
            _transactionService =  transactionService;
        }
    }
}
