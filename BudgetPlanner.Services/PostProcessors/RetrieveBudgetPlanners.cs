using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.PostProcessors
{
    public class RetrieveBudgetPlanners : IRequestPostProcessor<RetrieveBudgetPlannersRequest, RetrieveBudgetPlannersResponse>
    {
        private readonly ITransactionProvider _transactionProvider;

        public async Task Process(RetrieveBudgetPlannersRequest request, RetrieveBudgetPlannersResponse response, CancellationToken cancellationToken)
        {
            foreach (var budget in response.BudgetPlanners)
                budget.Balance = await _transactionProvider.GetBalance(budget.Id);
            
        }

        public RetrieveBudgetPlanners(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }
    }
}
