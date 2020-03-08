using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IClaimService
    {
        Task<Claim> GetClaim(string claimType, CancellationToken cancellationToken);
        Task<IEnumerable<AccountClaim>> GetAccountClaims(Claim foundClaim, CancellationToken cancellationToken);
        IEnumerable<Account> GetAccounts(IEnumerable<AccountClaim> accountClaims);
        Task<IEnumerable<AccountClaim>> GetAccountClaims(int accountId, CancellationToken cancellationToken);
        IEnumerable<Claim> GetClaims(IEnumerable<Claim> claims, IEnumerable<AccountClaim> accountClaims);
        Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellationToken);
        Claim GetClaim(IEnumerable<Claim> claims, string claimType);
        Task<Claim> SaveClaim(Claim claim, CancellationToken cancellationToken, bool saveChanges = true);
        Task<AccountClaim> SaveAccountClaim(AccountClaim accountClaim, CancellationToken cancellationToken, bool saveChanges = true);
    }
}
