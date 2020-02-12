using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Providers;
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
        IUserStore<Account>
    {
        private readonly IEncryptionProvider _encryptionHelper;
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IClaimService _claimService;

        private async Task<Domains.Data.Account> GetAccount(int userId)
        {
            var foundAccount = await _budgetPlannerCacheProvider.GetAccount(userId);
            return foundAccount;
        }

        private async Task<Domains.Data.Account> GetAccountByEmailAddress(string emailAddress)
        {
            var account = new Account { EmailAddress = emailAddress };
            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(account);
            var foundAccount = await _budgetPlannerCacheProvider.GetAccount(encryptedAccount.EmailAddress);
            return foundAccount;
        }

        public async Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            if(await FindByNameAsync(user.EmailAddress, cancellationToken) != null)
                return IdentityResult.Failed(IdentityErrors.DuplicateAccount);

            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);
            encryptedAccount.Active = true;

            await _accountService.SaveAccount(encryptedAccount, cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            var foundUser = await GetAccount(user.Id);
            if(foundUser == null)
                return IdentityResult.Failed(IdentityErrors.AccountNotFound);

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
            if(!int.TryParse(userId, out var id))
                return default;

            var foundAccount = await GetAccount(id);

            if(foundAccount == null)
                return default;

            return await _encryptionHelper.Decrypt<Domains.Data.Account, Account>(foundAccount);
        }

        public async Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var account = await GetAccountByEmailAddress(normalizedUserName);

            if(account == null)
                return default;

            return await _encryptionHelper.Decrypt<Domains.Data.Account, Account>(account);
        }

        public async Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return await GetUserNameAsync(user, cancellationToken);
        }

        public async Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.EmailAddress);
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

        public async Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            await SetNormalizedUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            var foundAccount = await GetAccountByEmailAddress(user.EmailAddress);
                
            if(foundAccount == null)
                return IdentityResult.Failed(IdentityErrors.AccountNotFound);

            var encryptedAccount = await _encryptionHelper.Encrypt<Account, Domains.Data.Account>(user);

            encryptedAccount.Id = foundAccount.Id;

            await _accountService.SaveAccount(encryptedAccount, cancellationToken);
            return IdentityResult.Success;
        }

        public AccountStore(IEncryptionProvider encryptionHelper, IBudgetPlannerCacheProvider budgetPlannerCacheProvider, 
            IAccountService accountService, IRoleService roleService, IClaimService claimService)
        {
            _encryptionHelper = encryptionHelper;
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _accountService = accountService;
            _roleService = roleService;
            _claimService = claimService;
        }
    }
}
