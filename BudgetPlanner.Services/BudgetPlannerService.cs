using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Enumerations;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class BudgetPlannerService : IBudgetPlannerService
    {
        private readonly IRepository<Budget> _budgetRepository;

        private IQueryable<Budget> DefaultBudgetQuery => _budgetRepository.Query(budget => budget.Active == true);

        public async Task<Budget> GetBudgetPlanner(string reference)
        {
            var budgetQuery = from budget in DefaultBudgetQuery
                              where budget.Reference == reference
                              select budget;

            return await budgetQuery.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Budget>> GetBudgetPlanners(DateTime lastUpdated, OrderBy orderBy = OrderBy.Descending)
        {
            var budgetQuery = from budget in DefaultBudgetQuery
                              where budget.LastUpdated == lastUpdated
                              select budget;

            if (orderBy == OrderBy.Descending)
                budgetQuery = from budget in budgetQuery
                              orderby budget.LastUpdated descending
                              select budget;

            if (orderBy == OrderBy.Ascending)
                budgetQuery = from budget in budgetQuery
                              orderby budget.LastUpdated ascending
                              select budget;

            return await budgetQuery.ToArrayAsync();
        }

        public BudgetPlannerService(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
    }
}
