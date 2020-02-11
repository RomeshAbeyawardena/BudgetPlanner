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
        Task<AccountClaim> GetAccountClaims(Claim foundClaim);
        IEnumerable<Account> GetAccounts(AccountClaim accountClaims);
    }
}
