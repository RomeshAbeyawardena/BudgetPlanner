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
    public class TransactionValidator : ValidatorBase<CreateTransactionRequest>
    {
        private readonly IBudgetPlannerService _budgetPlannerService;

        public TransactionValidator(IAccountService accountService,
            IBudgetPlannerService budgetPlannerService)
            : base(accountService)
        {
            _budgetPlannerService = budgetPlannerService;
            RuleFor(model => model.AccountId)
                .MustAsync(BeAValidAccount);

            RuleFor(model => model.BudgetId)
                .MustAsync(BudgetPlannerExists);

            RuleFor(model => model.Amount)
                .GreaterThan(decimal.Zero);

            RuleFor(model => model.Description)
                .MaximumLength(320);
        }

        private async Task<bool> BudgetPlannerExists(int budgetPlannerId, CancellationToken cancellationToken)
        {
            var budgetPlanner = await _budgetPlannerService.GetBudgetPlanner(budgetPlannerId, cancellationToken);
            return budgetPlanner != null;
        }
    }
}
