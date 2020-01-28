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
    public class RetrieveTransactionTypes : IRequestHandler<RetrieveTransactionTypesRequest, RetrieveTransactionTypesResponse>
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public async Task<RetrieveTransactionTypesResponse> Handle(RetrieveTransactionTypesRequest request, CancellationToken cancellationToken)
        {
            return new RetrieveTransactionTypesResponse { TransactionTypes = await _transactionTypeService.GetTransactionTypes() };
        }

        public RetrieveTransactionTypes(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }
    }
}
