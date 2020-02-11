using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Stores
{
    public partial class AccountStore : IUserClaimStore<Account>
    {
        private async Task<IEnumerable<Claim>> GetClaims(int accountId)
        {
            var accountRoles = await _roleService.GetAccountRoles(accountId);
            var roles = accountRoles;
        }

        public async Task AddClaimsAsync(Account user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var account = await GetAccount(user.Id);
        }

        public async Task<IList<Claim>> GetClaimsAsync(Account user, CancellationToken cancellationToken)
        {
            var account = await GetAccount(user.Id);
            throw new NotImplementedException();
        }

        public Task<IList<Account>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            
        }

        public Task RemoveClaimsAsync(Account user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(Account user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
