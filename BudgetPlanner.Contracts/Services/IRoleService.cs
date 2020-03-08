using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken);
        IEnumerable<Role> GetRoles(IEnumerable<AccountRole> accountRoles);
        IEnumerable<Role> GetRoles(IEnumerable<Role> roles, IEnumerable<string> roleNames);
        Task<Role> SaveRole(Role role, CancellationToken cancellationToken);
        Task<AccountRole> SaveAccountRole(AccountRole accountRole, CancellationToken cancellationToken);
        Task<IEnumerable<AccountRole>> GetAccountRoles(int accountId, CancellationToken cancellationToken);
        Task<IEnumerable<AccountRole>> GetAccountRoles(IEnumerable<Role> roles, CancellationToken cancellationToken);
        Task<Role> GetRole(int id, CancellationToken cancellationToken = default);
        Task<Role> GetRole(string normalizedRoleName, CancellationToken cancellationToken);
    }
}
