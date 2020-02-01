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
        private readonly ITransactionTypeService _transactionTypeService;

        public async Task<IEnumerable<TransactionType>> GetTransactionTypes()
        {
            return await _cacheProvider.GetOrSet(CacheType.DistributedMemoryCache, 
                CacheConstants.TransactionTypes,
                async() => await _transactionTypeService.GetTransactionTypes());
        }

        public BudgetPlannerCacheProvider(ICacheProvider cacheProvider, ITransactionTypeService transactionTypeService)
        {
            _cacheProvider = cacheProvider;
            _transactionTypeService = transactionTypeService;
        }
    }
}
