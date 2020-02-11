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
        Task<IEnumerable<Role>> GetRoles();
        IEnumerable<Role> GetRoles(IEnumerable<AccountRole> accountRoles);
        IEnumerable<Role> GetRoles(IEnumerable<Role> roles, IEnumerable<string> roleNames);
        Task<Role> SaveRole(Role role);
        Task<AccountRole> SaveAccountRole(AccountRole accountRole);
        Task<IEnumerable<AccountRole>> GetAccountRoles(int accountId);
        Task<IEnumerable<AccountRole>> GetAccountRoles(IEnumerable<Role> roles);
        Task<Role> GetRole(int id, CancellationToken cancellationToken = default);
        Task<Role> GetRole(string normalizedRoleName);
    }
}
