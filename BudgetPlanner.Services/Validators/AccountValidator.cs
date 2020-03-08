using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using DNI.Core.Contracts.Providers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class AccountValidator: AbstractValidator<RegisterAccountRequest>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IAccountService _accountService;

        public AccountValidator(IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _encryptionProvider = encryptionProvider;
            _accountService = accountService;

            RuleFor(model => model.Account)
                .NotNull();

            RuleFor(model => model.Account.EmailAddress)
                .EmailAddress();

            RuleFor(model => model.Account)
                .MustAsync(BeUnique)
                .WithMessage(model => string.Format("{0} is already taken", model.Account.EmailAddress))
                .WithName(property => property.Account.EmailAddress)
                .OverridePropertyName(property => property.Account.EmailAddress);

            RuleFor(model => model.Account.FirstName)
                .MinimumLength(3)
                .MaximumLength(32);

            RuleFor(model => model.Account.LastName)
                .MinimumLength(3)
                .MaximumLength(800);
        }

        private async Task<bool> BeUnique(Account account, CancellationToken cancellationToken)
        {
            var encryptedAccount = await _encryptionProvider
                .Encrypt<Account, Domains.Data.Account>(account);

            var foundAccount = await _accountService.GetAccount(encryptedAccount.EmailAddress, cancellationToken);

            return foundAccount == null;
        }
    }
}
