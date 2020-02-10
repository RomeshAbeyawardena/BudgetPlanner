using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Contracts.Providers;
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
    public partial class AccountStore : 
        IUserStore<Account>, 
        IUserPasswordStore<Account>
        
    {
        private readonly IEncryptionProvider _encryptionHelper;
        private readonly IAccountService _accountService;

        private async Task<Domains.Data.Account> GetAccount(int userId)
        {
            var foundAccount = await _accountService.GetAccount(userId, EntityUsage.SaveToDatabase);
            return foundAccount;
        }

        private async Task<Domains.Data.Account> GetAccountByEmailAddress(string emailAddress)
        {
            var account = new Account { EmailAddress = emailAddress };
            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(account);
            var foundAccount = await _accountService.GetAccount(encryptedAccount.EmailAddress);
            return foundAccount;
        }

        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            if(await FindByNameAsync(user.EmailAddress, cancellationToken) != null)
                return IdentityResult.Failed(AccountStoreIdentityErrors.DuplicateAccount);

            user.Active = true;
            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);
            await _accountService.SaveAccount(encryptedAccount, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            var foundUser = await GetAccount(user.Id);
            if(foundUser == null)
                return IdentityResult.Failed(AccountStoreIdentityErrors.AccountNotFound);

            foundUser.Active = false;
            await _accountService.SaveAccount(foundUser);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool gc)
        {

        }

        public async Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var foundAccount = await GetAccountByEmailAddress(userId);
            if(foundAccount == null)
                return default;

            return await _encryptionHelper.Decrypt<Domains.Data.Account, Account>(foundAccount);
        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return await GetUserNameAsync(user, cancellationToken);
        }

        public async Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.EmailAddress);
        }

        public async Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            var account = await GetAccountByEmailAddress(user.EmailAddress);

            if(account == null)
                return null;

            var decryptedAccount = await _encryptionHelper.Decrypt<Domains.Data.Account, Account>(account);

            return $"{decryptedAccount.FirstName} {decryptedAccount.LastName}";
        }

        public async Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            var splitName = normalizedName.Split(' ');

            if(splitName.Length != 2)
                return;

            var foundAccount = await GetAccountByEmailAddress(user.EmailAddress);

            if(foundAccount == null)
                return;

            user.FirstName = splitName[0];
            user.LastName = splitName[1];

            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);

            foundAccount.FirstName = encryptedAccount.FirstName;
            foundAccount.LastName = encryptedAccount.LastName;

            await _accountService.SaveAccount(foundAccount, cancellationToken);
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            var foundAccount = await GetAccountByEmailAddress(user.EmailAddress);
                
            if(foundAccount == null)
                return IdentityResult.Failed(AccountStoreIdentityErrors.AccountNotFound);

            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);

            encryptedAccount.Id = foundAccount.Id;

            await _accountService.SaveAccount(encryptedAccount, cancellationToken);
            return IdentityResult.Success;
        }

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
            if(foundAccount == null)
                return default;
            user.Id = foundAccount.Id;

            return Convert.ToBase64String(foundAccount.Password);
        }

        public async Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }

        public AccountStore(IEncryptionProvider encryptionHelper, IAccountService accountService)
        {
            _encryptionHelper = encryptionHelper;
            _accountService = accountService;
        }
    }
}
