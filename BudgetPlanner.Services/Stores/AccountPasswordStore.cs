using BudgetPlanner.Domains.Dto;
using DNI.Shared.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Stores
{
    public partial class AccountStore : IUserPasswordStore<Account>
    {

        public async Task SetPasswordHashAsync(Account user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash
                .ToBase64String(Encoding.UTF8)
                .GetBytes(Encoding.UTF8);

            var account = await GetAccountByEmailAddress(user.EmailAddress);
            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);
            account.Password = encryptedAccount.Password;
            await _accountService.SaveAccount(account);
        }

        public async Task<string> GetPasswordHashAsync(Account user, CancellationToken cancellationToken)
        {
            var foundAccount = await GetAccountByEmailAddress(user.EmailAddress);
            if (foundAccount == null)
                return default;
            user.Id = foundAccount.Id;

            return Convert.ToBase64String(foundAccount.Password);
        }

        public async Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }

    }
}
