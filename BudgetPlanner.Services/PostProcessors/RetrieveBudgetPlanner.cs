using BudgetPlanner.Contracts.Providers;
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
    public class RetrieveBudgetPlanner : IRequestPostProcessor<RetrieveBudgetPlannerRequest, RetrieveBudgetPlannerResponse>
    {
        private readonly ITransactionProvider _transactionProvider;

        public async Task Process(RetrieveBudgetPlannerRequest request, RetrieveBudgetPlannerResponse response, CancellationToken cancellationToken)
        {
            if(response.BudgetPlanner == null)
                return;

            response.Amount = await _transactionProvider.GetBalance(response.BudgetPlanner.Id);
            response.IsSuccessful = true;
        }

        public RetrieveBudgetPlanner(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }
    }
}
