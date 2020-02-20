using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Providers
{
    public interface IBudgetPlannerCacheProvider
    {
        Task<IEnumerable<TransactionType>> GetTransactionTypes();
        Task<IEnumerable<Tag>> GetTags();
        Task<Account> GetAccount(int accountId);
        Task<Account> GetAccount(IEnumerable<byte> emailAddress);
        Task<IEnumerable<Role>> GetRoles();
        Task<IEnumerable<AccessType>> GetAccessTypes();
        Task<AccessType> GetAccessType(string name);
        Task<AccessType> GetAccessType(int id);
        Task<Role> GetRole(int id);
        Task<Role> GetRole(string name);
    }
}
