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
        private async Task<IEnumerable<Account>> GetAccountsWithClaims(SecurityClaim claim)
        {
            var foundClaim = await _claimService.GetClaim(claim.Type);

            if(foundClaim == null)
                return default;

            var accountClaims = await _claimService.GetAccountClaims(foundClaim);

            return _claimService.GetAccounts(accountClaims);
        }

        private async Task<IEnumerable<AccountClaim>> GetAccountClaims(int accountId)
        {
            return await _claimService.GetAccountClaims(accountId);
        }

        private async Task<IEnumerable<SecurityClaim>> GetClaims(int accountId)
        {
            var accountRoles = await _roleService.GetAccountRoles(accountId);
            var roles = _roleService.GetRoles(accountRoles);

            var claims = new List<SecurityClaim>();

            //Add role claims
            claims.Add(new SecurityClaim(ClaimConstants.RolesClaim, string.Join(",", roles.Select(role => role.Name)) ));

            var accountClaims = await _claimService.GetAccountClaims(accountId);
            
            //Add other claims
            foreach(var accountClaim in accountClaims)
            {
                claims.Add(new SecurityClaim(accountClaim.Claim.Name, accountClaim.Value));
            }

            return claims.ToArray();
        }

        public async Task AddClaimsAsync(Domains.Dto.Account user, IEnumerable<SecurityClaim> claims, CancellationToken cancellationToken)
        {
            if(claims.Any(claim => claim.Type == ClaimConstants.RolesClaim))
                throw new NotSupportedException();

            var existingClaims = await _claimService.GetClaims();
            
            var securityClaims = await GetAccountClaims(user.Id);
            
            foreach(var securityClaim in claims)
            {
                var existingClaim = _claimService.GetClaim(existingClaims, securityClaim.Type);

                var existingSecurityClaim = securityClaims.FirstOrDefault(claim => claim.Claim.Name == securityClaim.Type);

                if (existingClaim == null)
                    existingClaim = await _claimService
                        .SaveClaim(new Domains.Data.Claim { Name = securityClaim.Type }, false);

                if (existingSecurityClaim == null)
                    await _claimService.SaveAccountClaim(new AccountClaim { AccountId = user.Id, Claim = existingClaim }, true);
                else
                {
                    existingSecurityClaim.Value = securityClaim.Value;
                    await _claimService.SaveAccountClaim(existingSecurityClaim, true);
                }
            }
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
