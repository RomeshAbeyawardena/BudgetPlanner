using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class BudgetLimitService : IBudgetLimitService
    {
        private readonly IRepository<BudgetLimit> _budgetLimitRepository;
        private IQueryable<BudgetLimit> DefaultQuery => _budgetLimitRepository.Query(budgetLimit => budgetLimit.Active, false);
        public async Task<BudgetLimit> GetBudgetLimit(int id, CancellationToken cancellationToken = default)
        {
            return await _budgetLimitRepository.Find(false, cancellationToken, id);
        }

        public BudgetLimit GetBudgetLimit(IEnumerable<BudgetLimit> budgetLimits, int budgetId, DateTime fromDate, DateTime? toDate = null)
        {
            var query = from budgetLimit in budgetLimits
                   where budgetLimit.BudgetId == budgetId 
                   && budgetLimit.FromDate >= fromDate
                   && !toDate.HasValue || budgetLimit.ToDate <= toDate
                   select budgetLimit;

            return query.FirstOrDefault();
        }

        public Task<IEnumerable<BudgetLimit>> GetBudgetLimits(int budgetId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BudgetLimit> SaveBudgetLimit(BudgetLimit budgetLimit, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public BudgetLimitService(IRepository<BudgetLimit> budgetLimitRepository)
        {
            _budgetLimitRepository = budgetLimitRepository;
        }
    }
}
