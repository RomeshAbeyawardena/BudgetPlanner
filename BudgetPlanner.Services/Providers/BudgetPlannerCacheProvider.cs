using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Providers
{
    public class BudgetPlannerCacheProvider : IBudgetPlannerCacheProvider
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IAccountService _accountService;
        private readonly IAccountAccessService _accountAccessService;
        private readonly IRoleService _roleService;
        private readonly ITransactionTypeService _transactionTypeService;

        public async Task<IEnumerable<TransactionType>> GetTransactionTypes(CancellationToken cancellationToken)
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache,
                CacheConstants.TransactionTypes,
                async (cancellationToken) => await _transactionTypeService.GetTransactionTypes(cancellationToken), false,
                cancellationToken);
        }

        public async Task<Account> GetAccount(int id, CancellationToken cancellationToken)
        {
            var account = await _cacheProvider.Get<Account>(CacheType.SessionCache, CacheConstants.CurrentAccount);

            if (account == null)
                return await SetAccount(id, cancellationToken);

            if(account.Id == id)
                return account;

            return await SetAccount(id, cancellationToken);
        }

        public async Task<Account> GetAccount(IEnumerable<byte> emailAddress, CancellationToken cancellationToken)
        {
            var account = await _cacheProvider.Get<Account>(CacheType.SessionCache, CacheConstants.CurrentAccount);

            if (account == null)
                return await SetAccount(emailAddress, cancellationToken);

            var originalHash = Convert.ToBase64String(account.EmailAddress);
            var newHash = Convert.ToBase64String(emailAddress.ToArray());

            if (originalHash == newHash)
                return account;

            return await SetAccount(emailAddress, cancellationToken);

        }

        private async Task<Account> SetAccount(int id, CancellationToken cancellationToken)
        {
            return await _cacheProvider
                .Set(CacheType.SessionCache,
                CacheConstants.CurrentAccount,
                async (cancellationToken) => await _accountService.GetAccount(id, EntityUsage.UseLocally), cancellationToken);
        }

        private async Task<Account> SetAccount(IEnumerable<byte> emailAddress, CancellationToken cancellationToken)
        {
            return await _cacheProvider
                .Set(CacheType.SessionCache,
                CacheConstants.CurrentAccount,
                async (cancellationToken) => await _accountService.GetAccount(emailAddress), cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken)
        {
            return await _cacheProvider
                .GetOrSet(CacheType.DistributedMemoryCache, CacheConstants.Roles, async(cancellationToken) => await _roleService.GetRoles());
        }

        public async Task<Role> GetRole(int id, CancellationToken cancellationToken)
        {
            var roles = await GetRoles(cancellationToken);
            return roles.SingleOrDefault(role => role.Id == id);
        }

        public async Task<Role> GetRole(string name, CancellationToken cancellationToken)
        {
            var roles = await GetRoles(cancellationToken);
            return roles.SingleOrDefault(role => role.Name == name);
        }

        public async Task<IEnumerable<AccessType>> GetAccessTypes(CancellationToken cancellationToken)
        {
            return await _cacheProvider
                .GetOrSet(CacheType.DistributedMemoryCache, CacheConstants.AccessTypes, 
                async(cancellationToken) => await _accountAccessService.GetAccessTypes());
        }

        public async Task<AccessType> GetAccessType(string name, CancellationToken cancellationToken)
        {
            var accessTypes = await GetAccessTypes(cancellationToken);
            return _accountAccessService.GetAccessType(accessTypes, name);
        }

        public async Task<AccessType> GetAccessType(int id, CancellationToken cancellationToken)
        {
            var accessTypes = await GetAccessTypes(cancellationToken);
            return _accountAccessService.GetAccessType(accessTypes, id);
        }

        public BudgetPlannerCacheProvider(ICacheProvider cacheProvider, 
            IAccountService accountService, 
            IAccountAccessService accountAccessService,
            IRoleService roleService,
            ITransactionTypeService transactionTypeService)
        {
            _cacheProvider = cacheProvider;
            _accountService = accountService;
            _accountAccessService = accountAccessService;
            _roleService = roleService;
            _transactionTypeService = transactionTypeService;
        }
    }
}
