using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Providers
{
    public interface IBudgetPlannerCacheProvider
    {
        Task<IEnumerable<TransactionType>> GetTransactionTypes();
    }
}
