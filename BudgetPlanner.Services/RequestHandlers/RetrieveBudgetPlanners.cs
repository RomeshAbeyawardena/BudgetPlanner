using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts;
using MediatR;
using BudgetPlanner.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RetrieveBudgetPlanners : IRequestHandler<RetrieveBudgetPlannersRequest, RetrieveBudgetPlannersResponse>
    {
        private readonly IMapperProvider _mapperProvider;
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<RetrieveBudgetPlannersResponse> Handle(RetrieveBudgetPlannersRequest request, CancellationToken cancellationToken)
        {
            var budgetPlanners = await _budgetPlannerService.GetBudgetPlanners(request.LastUpdated);
            var budgets = _mapperProvider.Map<Domains.Data.Budget, Domains.Dto.Budget>(budgetPlanners);

            return new RetrieveBudgetPlannersResponse { BudgetPlanners = budgets };
        }

        public RetrieveBudgetPlanners(IMapperProvider mapperProvider, IBudgetPlannerService budgetPlannerService)
        {
            _mapperProvider = mapperProvider;
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
