using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Contracts;
using MediatR;
using BudgetPlanner.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Domains;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RetrieveBudgetPlanners : IRequestHandler<RetrieveBudgetPlannersRequest, RetrieveBudgetPlannersResponse>
    {
        private readonly IMapperProvider _mapperProvider;
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<RetrieveBudgetPlannersResponse> Handle(RetrieveBudgetPlannersRequest request, CancellationToken cancellationToken)
        {
            var budgetPlanners = await _budgetPlannerService.GetBudgetPlanners(request.AccountId, request.LastUpdated);
            var budgets = _mapperProvider.Map<Domains.Data.Budget, Domains.Dto.Budget>(budgetPlanners);

            return Response.Success<RetrieveBudgetPlannersResponse>(budgets);
        }

        public RetrieveBudgetPlanners(IMapperProvider mapperProvider, IBudgetPlannerService budgetPlannerService)
        {
            _mapperProvider = mapperProvider;
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
