using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IClaimService
    {
        Task<Claim> GetClaim(string claimType);
        Task<IEnumerable<AccountClaim>> GetAccountClaims(Claim foundClaim);
        IEnumerable<Account> GetAccounts(IEnumerable<AccountClaim> accountClaims);
        Task<IEnumerable<AccountClaim>> GetAccountClaims(int accountId);
        IEnumerable<Claim> GetClaims(IEnumerable<Claim> claims, IEnumerable<AccountClaim> accountClaims);
        Task<IEnumerable<Claim>> GetClaims();
        Claim GetClaim(IEnumerable<Claim> claims, string claimType);
        Task<Claim> SaveClaim(Claim claim, bool saveChanges = true);
        Task SaveAccountClaim(AccountClaim accountClaim, bool v);
    }
}
