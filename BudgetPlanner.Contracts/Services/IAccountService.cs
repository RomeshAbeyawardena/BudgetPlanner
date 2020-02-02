using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IAccountService
    {
        Task<Account> SaveAccount(Account account);
        Task<Account> GetAccount(IEnumerable<byte> encryptedEmailAddress);
        Task<Account> GetAccount(int accountId);
    }
}
