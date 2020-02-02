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
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;

        public IQueryable<Account> DefaultAccountQuery => _accountRepository.Query(account => account.IsActive);

        public async Task<Account> GetAccount(IEnumerable<byte> encryptedEmailAddress)
        {
            var accountQuery = from account in DefaultAccountQuery
                               where account.EmailAddress == encryptedEmailAddress
                               select account;

            return await accountQuery.SingleOrDefaultAsync()
        }

        public async Task<Account> SaveAccount(Account account)
        {
            return await _accountRepository.SaveChanges(account);
        }

        public AccountService(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}
