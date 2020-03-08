using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Enumerations;
using DNI.Core.Contracts;
using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<Budget> GetBudgetPlanner(string reference, CancellationToken cancellationToken)
        {
            return await _budgetRepository.For(GetBudgetReferenceQuery(reference))
                .ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Budget>> GetBudgetPlanners(int accountId, DateTime lastUpdated, 
            CancellationToken cancellationToken,
            OrderBy orderBy = OrderBy.Descending)
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

            return await _budgetRepository.For(budgetQuery)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<bool> IsReferenceUnique(string uniqueReference, CancellationToken cancellationToken)
        {
            return !await _budgetRepository.For(GetBudgetReferenceQuery(uniqueReference))
                .AnyAsync(cancellationToken);
        }

        public async Task<Budget> Save(Budget budgetPlanner, CancellationToken cancellationToken)
        {
            return await _budgetRepository.SaveChanges(budgetPlanner, cancellationToken: cancellationToken);
        }

        public async Task<Budget> GetBudgetPlanner(int id, CancellationToken cancellationToken)
        {
            return await _budgetRepository.Find(keys: id, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<BudgetPlannerStat>> GetBudgetPlannerStats(int budgetId, DateTime fromDate, DateTime toDate,
            CancellationToken cancellationToken)
        {
            return await _budgetRepository.For(_budgetRepository
                .FromQuery<BudgetPlannerStat>("SELECT * FROM [dbo].[fn_GetBudgetStats] (@budgetId, @fromDate, @toDate)",
                new SqlParameter(nameof(budgetId), budgetId),
                new SqlParameter(nameof(fromDate), fromDate),
                new SqlParameter(nameof(toDate), toDate))
                .OrderByDescending(budgetStat => budgetStat.Date))
                .ToArrayAsync(cancellationToken);
        }

        public BudgetPlannerService(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
    }
}
