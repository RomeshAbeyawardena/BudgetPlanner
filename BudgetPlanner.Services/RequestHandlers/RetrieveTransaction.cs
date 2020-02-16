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
    public class RetrieveTransaction : IRequestHandler<RetrieveTransactionRequest, RetrieveTransactionResponse>
    {
        private readonly ITransactionService _transactionService;

        public async Task<RetrieveTransactionResponse> Handle(RetrieveTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetTransaction(request.TransactionId, cancellationToken);

            if(transaction == null)
                return new RetrieveTransactionResponse { IsSuccessful = false };

            return new RetrieveTransactionResponse
            {
                IsSuccessful = true,
                Result = transaction
            };
        }

        public RetrieveTransaction(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}
