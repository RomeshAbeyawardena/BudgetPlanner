using System;
using System.Collections.Generic;
using BudgetPlanner.Domains.Data;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IBudgetPlannerService
    {
        Task<IEnumerable<Budget>> GetBudgetPlanners(DateTime lastUpdated);
        Budget GetBudgetPlanner(string reference);
    }
}
