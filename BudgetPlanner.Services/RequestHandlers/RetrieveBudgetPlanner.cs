using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
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
    public class RetrieveBudgetPlanner : IRequestHandler<RetrieveBudgetPlannerRequest, RetrieveBudgetPlannerResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<RetrieveBudgetPlannerResponse> Handle(RetrieveBudgetPlannerRequest request, CancellationToken cancellationToken)
        {
            Budget budget;

            if(request.BudgetPlannerId.HasValue)
                budget = await _budgetPlannerService.GetBudgetPlanner(request.BudgetPlannerId.Value);
            else
                budget = await _budgetPlannerService.GetBudgetPlanner(request.Reference);

            if(budget.AccountId == request.AccountId)
                return Response.Success<RetrieveBudgetPlannerResponse>(budget);

            return Response.Failed<RetrieveBudgetPlannerResponse>();
        }

        public RetrieveBudgetPlanner(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
