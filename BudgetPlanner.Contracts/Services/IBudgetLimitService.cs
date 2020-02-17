using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IBudgetLimitService
    {
        Task<BudgetLimit> SaveBudgetLimit(BudgetLimit budgetLimit, bool saveChanges = true, CancellationToken cancellationToken = default);
        Task<BudgetLimit> GetBudgetLimit(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BudgetLimit>> GetBudgetLimits(int budgetId, CancellationToken cancellationToken = default);
        BudgetLimit GetBudgetLimit(IEnumerable<BudgetLimit> budgetLimits, int budgetId, DateTime fromDate, DateTime? toDate);
    }
}
