using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Stores
{
    public partial class AccountStore : IUserLockoutStore<Domains.Dto.Account>
    {
        private async Task<AccessType> GetAccessType(string name)
        {
            return await _budgetPlannerCacheProvider.GetAccessType(name);
        }
        private async Task <IEnumerable<AccountAccess>> GetLoginAccessFailed(int accountId)
        {
            var fromDate = _clockProvider.DateTime.AddMinutes(_applicationSettings.AccountLockoutPeriodInMinutes);
            var loginAccessType = GetAccessType(DataConstants.LoginAccess);
            return (await _accountAccessService.GetAccountAccess(accountId, loginAccessType.Id, 
                fromDate, false));
        }
        public async Task<int> GetAccessFailedCountAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            return (await GetLoginAccessFailed(user.Id)).Count();
        }

        public Task<bool> GetLockoutEnabledAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            return (await GetLoginAccessFailed(user.Id)).FirstOrDefault()?.Created;
        }

        public Task<int> IncrementAccessFailedCountAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            var loginAccessType = GetAccessType(DataConstants.LoginAccess);
        }

        public Task ResetAccessFailedCountAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(Domains.Dto.Account user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(Domains.Dto.Account user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
