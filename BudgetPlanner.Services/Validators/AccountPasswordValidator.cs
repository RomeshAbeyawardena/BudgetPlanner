using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Services.Stores;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class AccountPasswordValidator : IPasswordValidator<Account>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public async Task<IdentityResult> ValidateAsync(UserManager<Account> manager, Account user, string password)
        {
            var foundUser = await manager.FindByIdAsync(user.EmailAddress);;
            if(foundUser == default)
                IdentityResult.Failed(AccountStoreIdentityErrors.AccountNotFound);

            var account = await _encryptionProvider.Encrypt<Account, Domains.Data.Account>(new Account { 
                Password = password.ToBase64String(Encoding.UTF8)
                .GetBytes(Encoding.UTF8) });

            if(foundUser.Password.SequenceEqual(account.Password))
                return IdentityResult.Success;

            return
                IdentityResult.Failed(AccountStoreIdentityErrors.AccountNotFound);
        }
    }
}
