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
    public class RetrieveBudgetPlanners : IRequestHandler<RetrieveBudgetPlannersRequest, RetrieveBudgetPlannersResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<RetrieveBudgetPlannersResponse> Handle(RetrieveBudgetPlannersRequest request, CancellationToken cancellationToken)
        {
            return new RetrieveBudgetPlannersResponse { BudgetPlanners =  await _budgetPlannerService.GetBudgetPlanners(request.LastUpdated) };
        }

        public RetrieveBudgetPlanners(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
