using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RetrieveBudgetPlannerStats : IRequestHandler<BudgetPlannerStatsRequest, BudgetPlannerStatsResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<BudgetPlannerStatsResponse> Handle(BudgetPlannerStatsRequest request, CancellationToken cancellationToken)
        {
            var budgetPlannerStats = await _budgetPlannerService
                .GetBudgetPlannerStats(request.BudgetId, request.FromDate, request.ToDate);
            return Response.Success<BudgetPlannerStatsResponse>(budgetPlannerStats);
        }

        public RetrieveBudgetPlannerStats(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
