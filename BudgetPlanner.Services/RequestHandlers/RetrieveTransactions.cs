using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Domains;
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

            var transactions = await transactionsPager
                    .GetPagedItems(builder => { 
                        builder.PageNumber = request.PageNumber;
                        builder.MaximumRowsPerPage = request.PageSize; 
                        builder.UseAsync = true;
                    }, cancellationToken: cancellationToken);

            var totalPages = await transactionsPager.GetTotalNumberOfPages(request.PageSize);

            var response = Response.Success<RetrieveTransactionsResponse>(transactions);
            response.TotalPages = totalPages;
            response.PageNumber = request.PageNumber;
            return response;

        }

        public RetrieveTransactions(ITransactionService transactionService)
        {
            _transactionService =  transactionService;
        }
    }
}
