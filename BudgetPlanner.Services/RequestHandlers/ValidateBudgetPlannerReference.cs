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
    public class ValidateBudgetPlannerReference : IRequestHandler<ValidateBudgetPlannerReferenceRequest, ValidateBudgetPlannerReferenceResponse>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public async Task<ValidateBudgetPlannerReferenceResponse> Handle(ValidateBudgetPlannerReferenceRequest request, CancellationToken cancellationToken)
        {
            return new ValidateBudgetPlannerReferenceResponse { IsUnique = await _budgetPlannerService.IsReferenceUnique(request.UniqueReference) };
        }

        public ValidateBudgetPlannerReference(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;
        }
    }
}
