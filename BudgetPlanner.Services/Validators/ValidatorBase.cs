using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public abstract class ValidatorBase<T> : AbstractValidator<T>
    {
        private readonly IAccountService _accountService;

        protected async Task<bool> BeAValidAccount(int accountId, CancellationToken cancellationToken)
        {
            return await _accountService.GetAccount(accountId, EntityUsage.UseLocally, cancellationToken) != null;
        }

        protected ValidatorBase(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }
}
