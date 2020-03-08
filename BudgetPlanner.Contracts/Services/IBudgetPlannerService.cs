using System;
using System.Collections.Generic;
using BudgetPlanner.Domains.Data;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Enumerations;
using System.Threading;

namespace BudgetPlanner.Contracts.Services
{
    public interface IBudgetPlannerService
    {
        Task<IEnumerable<Budget>> GetBudgetPlanners(int accountId, DateTime lastUpdated, CancellationToken cancellationToken, OrderBy orderBy = OrderBy.Descending);
        Task<Budget> GetBudgetPlanner(string reference, CancellationToken cancellationToken);
        Task<Budget> GetBudgetPlanner(int id, CancellationToken cancellationToken);
        Task<bool> IsReferenceUnique(string uniqueReference, CancellationToken cancellationToken);
        Task<IEnumerable<BudgetPlannerStat>> GetBudgetPlannerStats(int budgetId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
        Task<Budget> Save(Budget budgetPlanner, CancellationToken cancellationToken);
    }
}
