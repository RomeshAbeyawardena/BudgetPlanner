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

        private IQueryable<Budget> DefaultBudgetQuery => _budgetRepository.Query(budget => budget.Active == true, false);
        private IQueryable<Budget> DefaultAccountBudgetQuery(int accountId) => from budget in DefaultBudgetQuery
                                                                              where budget.AccountId == accountId
                                                                              select budget;

        private IQueryable<Budget> GetBudgetReferenceQuery(string reference) => from budget in DefaultBudgetQuery
                                                                  where budget.Reference == reference
                                                                  select budget;

        public async Task<Budget> GetBudgetPlanner(string reference)
        {
            return await GetBudgetReferenceQuery(reference)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Budget>> GetBudgetPlanners(int accountId, DateTime lastUpdated, OrderBy orderBy = OrderBy.Descending)
        {
            var budgetQuery = from budget in DefaultAccountBudgetQuery(accountId)
                              where budget.LastUpdated >= lastUpdated
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

        public async Task<bool> IsReferenceUnique(string uniqueReference)
        {
            return !await GetBudgetReferenceQuery(uniqueReference)
                .AnyAsync();
        }

        public async Task<Budget> Save(Budget budgetPlanner)
        {
            return await _budgetRepository.SaveChanges(budgetPlanner);
        }

        public async Task<Budget> GetBudgetPlanner(int id)
        {
            return await _budgetRepository.Find(keys: id);
        }

        public BudgetPlannerService(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
    }
}
