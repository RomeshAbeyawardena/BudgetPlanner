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
    public class CreateBudgetTransaction : IRequestPostProcessor<CreateTransactionRequest, CreateTransactionResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task Process(CreateTransactionRequest request, CreateTransactionResponse response, CancellationToken cancellationToken)
        {
            var budget = await _budgetPlannerService.GetBudgetPlanner(request.BudgetId);
            response.Reference = budget.Reference;
            budget.LastTransactionId = response.Result.Id;
            await _budgetPlannerService.Save(budget);
        }

        public CreateBudgetTransaction(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
