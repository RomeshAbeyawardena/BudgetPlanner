using System;
using System.Collections.Generic;
using BudgetPlanner.Domains.Data;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Enumerations;

namespace BudgetPlanner.Contracts.Services
{
    public interface IBudgetPlannerService
    {
        Task<IEnumerable<Budget>> GetBudgetPlanners(DateTime lastUpdated, OrderBy orderBy = OrderBy.Descending);
        Task<Budget> GetBudgetPlanner(string reference);
        Task<bool> IsReferenceUnique(string uniqueReference);
    }
}
