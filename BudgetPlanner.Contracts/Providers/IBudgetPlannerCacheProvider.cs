using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Providers
{
    public interface IBudgetPlannerCacheProvider
    {
        Task<IEnumerable<TransactionType>> GetTransactionTypes(CancellationToken cancellationToken);
        Task<Account> GetAccount(int accountId, CancellationToken cancellationToken);
        Task<Account> GetAccount(IEnumerable<byte> emailAddress, CancellationToken cancellationToken);
        Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken);
        Task<IEnumerable<AccessType>> GetAccessTypes(CancellationToken cancellationToken);
        Task<AccessType> GetAccessType(string name, CancellationToken cancellationToken);
        Task<AccessType> GetAccessType(int id, CancellationToken cancellationToken);
        Task<Role> GetRole(int id, CancellationToken cancellationToken);
        Task<Role> GetRole(string name, CancellationToken cancellationToken);
    }
}
