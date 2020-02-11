using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<Claim> _claimRepository;
        private readonly IRepository<AccountClaim> _accountClaimRepository;

        private IQueryable<Claim> DefaultClaimQuery => _claimRepository.Query(claim => claim.Active, false);
        private IQueryable<AccountClaim> DefaultAccountClaimQuery => _accountClaimRepository.Query(accountClaim => accountClaim.Active, false);

        public async Task<IEnumerable<AccountClaim>> GetAccountClaims(Claim claim)
        {
            var accountClaimQuery = from accountClaim in DefaultAccountClaimQuery
                                    where accountClaim.ClaimId == claim.Id
                                    select accountClaim;

            return await accountClaimQuery
                .Include(accountClaim => accountClaim.Account)
                .Include(accountClaim => accountClaim.Claim)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<AccountClaim>> GetAccountClaims(int accountId)
        {
            var accountClaimQuery = from accountClaim in DefaultAccountClaimQuery
                                    where accountClaim.AccountId == accountId
                                    select accountClaim;

            return await accountClaimQuery
                .Include(accountClaim => accountClaim.Claim)
                .ToArrayAsync();
        }

        public IEnumerable<Account> GetAccounts(IEnumerable<AccountClaim> accountClaims)
        {
            return accountClaims.Select(accountClaim => accountClaim.Account);
        }

        public async Task<Claim> GetClaim(string claimType)
        {
            var claimQuery = from claim in DefaultClaimQuery
                             where claim.Name == claimType
                             select claim;

            return await claimQuery.FirstOrDefaultAsync();
        }

        public Claim GetClaim(IEnumerable<Claim> claims, string claimType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> GetClaims(IEnumerable<Claim> claims, IEnumerable<AccountClaim> accountClaims)
        {
            var claimsQuery = from accountClaim in accountClaims
                              where claims.Any(claim => claim.Id == accountClaim.ClaimId)
                              select accountClaim.Claim;

            return claimsQuery.ToArray();
        }

        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await DefaultClaimQuery.ToArrayAsync();
        }

        public Task SaveAccountClaim(AccountClaim accountClaim, bool v)
        {
            throw new NotImplementedException();
        }

        public Task<Claim> SaveClaim(Claim claim, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public ClaimService(IRepository<Claim> claimRepository, IRepository<AccountClaim> accountClaimRepository)
        {
            _claimRepository = claimRepository;
            _accountClaimRepository = accountClaimRepository;
        }
    }
}
