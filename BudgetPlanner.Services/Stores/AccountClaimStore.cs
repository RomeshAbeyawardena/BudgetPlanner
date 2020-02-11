using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SecurityClaim = System.Security.Claims.Claim;

namespace BudgetPlanner.Services.Stores
{
    public partial class AccountStore : IUserClaimStore<Domains.Dto.Account>
    {
        private async Task<IEnumerable<Role>> GetRoleNamesFromSeparatedList(char separator, string roles)
        {
            var existingRoles = await _roleService.GetRoles();
            var splitString = roles.Split(separator);
            return _roleService.GetRoles(existingRoles, splitString);
        }

        private async Task<IEnumerable<Account>> GetAccountsWithClaims(SecurityClaim claim)
        {
            if(claim.Type == ClaimConstants.RolesClaim)
            {
                var roles = await GetRoleNamesFromSeparatedList(',', claim.Value);
                var accountRoles = await _roleService.GetAccountRoles(roles);
                return accountRoles.Select(accountRole => accountRole.Account);
            }
            
            var foundClaim = await _claimService.GetClaim(claim.Type);

            if(foundClaim == null)
                return default;

            var accountClaims = await _claimService.GetAccountClaims(foundClaim);

            return _claimService.GetAccounts(accountClaims);
        }

        private async Task<IEnumerable<SecurityClaim>> GetClaims(int accountId)
        {
            var accountRoles = await _roleService.GetAccountRoles(accountId);
            var roles = _roleService.GetRoles(accountRoles);

            var claims = new List<SecurityClaim>();

            claims.Add(new SecurityClaim(ClaimConstants.RolesClaim, string.Join(",", roles.Select(role => role.Name)) ));

            return claims.ToArray();
        }

        public async Task AddClaimsAsync(Domains.Dto.Account user, IEnumerable<SecurityClaim> claims, CancellationToken cancellationToken)
        {
            var account = await GetAccount(user.Id);
        }

        public async Task<IList<SecurityClaim>> GetClaimsAsync(Domains.Dto.Account user, CancellationToken cancellationToken)
        {
            return (await GetClaims(user.Id)).ToList();
        }

        public async Task<IList<Domains.Dto.Account>> GetUsersForClaimAsync(SecurityClaim claim, CancellationToken cancellationToken)
        {
            var accounts = await GetAccountsWithClaims(claim);
            var decryptedAccounts =  await _encryptionHelper.Decrypt<Account, Domains.Dto.Account>(accounts);
            return decryptedAccounts.ToList();
        }

        public Task RemoveClaimsAsync(Domains.Dto.Account user, IEnumerable<SecurityClaim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(Domains.Dto.Account user, SecurityClaim claim, SecurityClaim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
