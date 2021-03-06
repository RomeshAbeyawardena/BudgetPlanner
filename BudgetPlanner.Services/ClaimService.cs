﻿using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<Claim> _claimRepository;
        private readonly IRepository<AccountClaim> _accountClaimRepository;

        private IQueryable<Claim> DefaultClaimQuery => _claimRepository.Query(claim => claim.Active, false);
        private IQueryable<AccountClaim> DefaultAccountClaimQuery => _accountClaimRepository.Query(accountClaim => accountClaim.Active, false);

        public async Task<IEnumerable<AccountClaim>> GetAccountClaims(Claim claim, CancellationToken cancellationToken)
        {
            var accountClaimQuery = from accountClaim in DefaultAccountClaimQuery
                                    where accountClaim.ClaimId == claim.Id
                                    select accountClaim;

            return await _accountClaimRepository.For(accountClaimQuery
                .Include(accountClaim => accountClaim.Account)
                .Include(accountClaim => accountClaim.Claim))
                .ToArrayAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountClaim>> GetAccountClaims(int accountId, CancellationToken cancellationToken)
        {
            var accountClaimQuery = from accountClaim in DefaultAccountClaimQuery
                                    where accountClaim.AccountId == accountId
                                    select accountClaim;

            return await _accountClaimRepository.For(accountClaimQuery) 
                .Include(accountClaim => accountClaim.Claim)
                .ToArrayAsync();
        }

        public IEnumerable<Account> GetAccounts(IEnumerable<AccountClaim> accountClaims)
        {
            return accountClaims.Select(accountClaim => accountClaim.Account);
        }

        public async Task<Claim> GetClaim(string claimType, CancellationToken cancellationToken)
        {
            var claimQuery = from claim in DefaultClaimQuery
                             where claim.Name == claimType
                             select claim;

            return await _claimRepository.For(claimQuery)
                .ToFirstOrDefaultAsync(cancellationToken);
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

        public async Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellationToken)
        {
            return await _claimRepository
                .For(DefaultClaimQuery)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<AccountClaim> SaveAccountClaim(AccountClaim accountClaim, CancellationToken cancellationToken, bool saveChanges = true)
        {
            return await _accountClaimRepository.SaveChanges(accountClaim, saveChanges);
        }

        public async Task<Claim> SaveClaim(Claim claim, CancellationToken cancellationToken, bool saveChanges = true)
        {
            return await _claimRepository.SaveChanges(claim, saveChanges);
        }

        public ClaimService(IRepository<Claim> claimRepository, IRepository<AccountClaim> accountClaimRepository)
        {
            _claimRepository = claimRepository;
            _accountClaimRepository = accountClaimRepository;
        }
    }
}
