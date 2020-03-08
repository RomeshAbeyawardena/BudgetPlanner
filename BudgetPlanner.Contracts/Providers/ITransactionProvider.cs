using BudgetPlanner.Domains.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Providers
{
    public interface ITransactionProvider
    {
        Task<decimal> GetBalance(int budgetPlannerId, CancellationToken cancellationToken, bool useRealtimeData = false);
    }
}
