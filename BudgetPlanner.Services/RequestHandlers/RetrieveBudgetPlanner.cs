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
    public class RetrieveBudgetPlanner : IRequestHandler<RetrieveBudgetPlannerRequest, RetrieveBudgetPlannerResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<RetrieveBudgetPlannerResponse> Handle(RetrieveBudgetPlannerRequest request, CancellationToken cancellationToken)
        {
            return new RetrieveBudgetPlannerResponse { BudgetPlanner = await _budgetPlannerService.GetBudgetPlanner(request.Reference) };
        }

        public RetrieveBudgetPlanner(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
