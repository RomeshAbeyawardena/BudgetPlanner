using BudgetPlanner.Contracts.Providers;
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
    public class AccountAccessValidator : ValidatorBase<CreateAccountAccessRequest>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;

        public AccountAccessValidator(IAccountService accountService, 
            IBudgetPlannerCacheProvider budgetPlannerCacheProvider)
            : base (accountService)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;

            RuleFor(model => model.AccountAccessModel.AccountId)
                .GreaterThan(0)
                .MustAsync(BeAValidAccount);

            RuleFor(model => model.AccountAccessModel.AccessTypeId)
                .GreaterThan(0)
                .MustAsync(AccountAccessExists);

        }

        private async Task<bool> AccountAccessExists(int accountAccessId, CancellationToken cancellationToken)
        {
            var accountAccess = await _budgetPlannerCacheProvider.GetAccessType(accountAccessId, cancellationToken);

            return accountAccess != null;
        }
    }
}
