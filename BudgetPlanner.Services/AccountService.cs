using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;

        public IQueryable<Account> DefaultAccountQuery => _accountRepository.Query(account => account.Active, false);

        public async Task<Account> GetAccount(IEnumerable<byte> encryptedEmailAddress, CancellationToken cancellationToken)
        {
            var emailAddressArray = encryptedEmailAddress.ToArray();
            var accountQuery = from account in DefaultAccountQuery
                               where account.EmailAddress == emailAddressArray
                               select account;

            return await _accountRepository.For(accountQuery)
                .ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Account> SaveAccount(Account account, CancellationToken cancellationToken)
        {
            return await _accountRepository.SaveChanges(account);
        }

        public async Task<Account> GetAccount(int accountId, EntityUsage findUsage, CancellationToken cancellationToken)
        {
            if(findUsage == EntityUsage.SaveToDatabase)
                return await _accountRepository.Find(false, CancellationToken.None, accountId);

            return await _accountRepository.For((from account in DefaultAccountQuery
                   where account.Id == accountId
                   select account)).ToSingleOrDefaultAsync(cancellationToken);
        }

        public AccountService(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}
