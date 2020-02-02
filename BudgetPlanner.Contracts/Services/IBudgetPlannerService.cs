﻿using System;
using System.Collections.Generic;
using BudgetPlanner.Domains.Data;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Enumerations;

namespace BudgetPlanner.Contracts.Services
{
    public interface IBudgetPlannerService
    {
        Task<IEnumerable<Budget>> GetBudgetPlanners(int accountId, DateTime lastUpdated, OrderBy orderBy = OrderBy.Descending);
        Task<Budget> GetBudgetPlanner(string reference);
        Task<Budget> GetBudgetPlanner(int id);
        Task<bool> IsReferenceUnique(string uniqueReference);
        Task<Budget> Save(Budget budgetPlanner);
    }
}
