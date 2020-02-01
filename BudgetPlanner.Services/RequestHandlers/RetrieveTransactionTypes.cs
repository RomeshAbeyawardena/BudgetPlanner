using BudgetPlanner.Contracts.Providers;
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
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;

        public async Task<RetrieveTransactionTypesResponse> Handle(RetrieveTransactionTypesRequest request, CancellationToken cancellationToken)
        {
            return new RetrieveTransactionTypesResponse { TransactionTypes = await _budgetPlannerCacheProvider.GetTransactionTypes() };
        }

        public RetrieveTransactionTypes(IBudgetPlannerCacheProvider budgetPlannerCacheProvider)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
        }
    }
}
