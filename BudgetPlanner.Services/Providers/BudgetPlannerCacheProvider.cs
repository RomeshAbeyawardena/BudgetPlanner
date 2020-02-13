using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<TransactionType>> GetTransactionTypes()
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache,
                CacheConstants.TransactionTypes,
                async () => await _transactionTypeService.GetTransactionTypes());
        }

        public async Task<Account> GetAccount(int id)
        {
            var account = await _cacheProvider.Get<Account>(CacheType.SessionCache, CacheConstants.CurrentAccount);

            if (account == null)
                return await SetAccount(id);

            if(account.Id == id)
                return account;

            return await SetAccount(id);
        }

        public async Task<Account> GetAccount(IEnumerable<byte> emailAddress)
        {
            var account = await _cacheProvider.Get<Account>(CacheType.SessionCache, CacheConstants.CurrentAccount);

            if (account == null)
                return await SetAccount(emailAddress);

            var originalHash = Convert.ToBase64String(account.EmailAddress);
            var newHash = Convert.ToBase64String(emailAddress.ToArray());

            if (originalHash == newHash)
                return account;

            return await SetAccount(emailAddress);

        }

        private async Task<Account> SetAccount(int id)
        {
            return await _cacheProvider
                .Set(CacheType.SessionCache,
                CacheConstants.CurrentAccount,
                async () => await _accountService.GetAccount(id, EntityUsage.UseLocally));
        }

        private async Task<Account> SetAccount(IEnumerable<byte> emailAddress)
        {
            return await _cacheProvider
                .Set(CacheType.SessionCache,
                CacheConstants.CurrentAccount,
                async () => await _accountService.GetAccount(emailAddress));
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _cacheProvider
                .GetOrSet(CacheType.DistributedMemoryCache, CacheConstants.Roles, async() => await _roleService.GetRoles());
        }

        public async Task<Role> GetRole(int id)
        {
            var roles = await GetRoles();
            return roles.SingleOrDefault(role => role.Id == id);
        }

        public async Task<Role> GetRole(string name)
        {
            var roles = await GetRoles();
            return roles.SingleOrDefault(role => role.Name == name);
        }

        public async Task<IEnumerable<AccessType>> GetAccessTypes()
        {
            return await _cacheProvider
                .GetOrSet(CacheType.DistributedMemoryCache, CacheConstants.AccessTypes, 
                async() => await _accountAccessService.GetAccessTypes());
        }

        public async Task<AccessType> GetAccessType(string name)
        {
            var accessTypes = await GetAccessTypes();
            return _accountAccessService.GetAccessType(accessTypes, name);
        }

        public async Task<AccessType> GetAccessType(int id)
        {
            var accessTypes = await GetAccessTypes();
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
