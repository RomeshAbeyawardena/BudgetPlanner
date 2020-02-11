using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        IEnumerable<Role> GetRoles(IEnumerable<AccountRole> accountRoles);
        Task<Role> SaveRole(Role role);
        Task<AccountRole> SaveAccountRole(AccountRole accountRole);
        Task<IEnumerable<AccountRole>> GetAccountRoles(int accountId);
    }
}
