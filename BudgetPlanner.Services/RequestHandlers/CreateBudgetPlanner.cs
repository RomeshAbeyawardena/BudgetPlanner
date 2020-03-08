using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Contracts;
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
    public class CreateBudgetPlanner : IRequestHandler<CreateBudgetPlannerRequest, CreateBudgetPlannerResponse>
    {
        private readonly IMapperProvider _mapperProvider;
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<CreateBudgetPlannerResponse> Handle(CreateBudgetPlannerRequest request, CancellationToken cancellationToken)
        {
            var budgetPlanner = _mapperProvider.Map<CreateBudgetPlannerRequest, Budget>(request);
            budgetPlanner = await _budgetPlannerService.Save(budgetPlanner, cancellationToken);

            return Response.Success<CreateBudgetPlannerResponse>(budgetPlanner);
        }

        public CreateBudgetPlanner(IMapperProvider mapperProvider, IBudgetPlannerService budgetPlannerService)
        {
            _mapperProvider = mapperProvider;
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
