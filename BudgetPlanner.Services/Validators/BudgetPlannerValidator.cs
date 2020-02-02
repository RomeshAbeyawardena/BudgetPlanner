using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class BudgetPlannerValidator : AbstractValidator<CreateBudgetPlannerRequest>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public BudgetPlannerValidator(IBudgetPlannerService budgetPlannerService)
        {
            _budgetPlannerService = budgetPlannerService;

            RuleFor(request => request.Reference)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(200)
                .MustAsync(BeUnique)
                .WithMessage(request => string.Format("Reference must be unique.", request.Reference));

            RuleFor(request => request.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(200)
                .MustAsync(BeUnique);
        }

        private async Task<bool> BeUnique(string reference, CancellationToken cancellationToken)
        {
            return await _budgetPlannerService.IsReferenceUnique(reference);
        }
    }
}
