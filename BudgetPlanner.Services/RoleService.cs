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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<AccountRole> _accountRoleRepository;

        public IQueryable<AccountRole> DefaultAccountRoleQuery => _accountRoleRepository.Query(accountRole => accountRole.Active, false);
        
        public IQueryable<Role> DefaultRoleQuery => _roleRepository.Query(role => role.Active, false);
        public async Task<IEnumerable<AccountRole>> GetAccountRoles(int accountId)
        {
            var accountRoleQuery = from accountRole in DefaultAccountRoleQuery.Include(accountRole => accountRole.Role)
            where accountRole.AccountId == accountId
            select accountRole;

            return await accountRoleQuery.ToArrayAsync();
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roleQuery = from role in DefaultRoleQuery
                            select role;

            return await roleQuery.ToArrayAsync();
        }

        public async Task<AccountRole> SaveAccountRole(AccountRole accountRole)
        {
            return await _accountRoleRepository.SaveChanges(accountRole);
        }

        public async Task<Role> SaveRole(Role role)
        {
            return await _roleRepository.SaveChanges(role);
        }

        public IEnumerable<Role> GetRoles(IEnumerable<AccountRole> accountRoles)
        {
            return accountRoles.Select(accountRole => accountRole.Role);
        }

        public IEnumerable<Role> GetRoles(IEnumerable<Role> roles, IEnumerable<string> roleNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountRole>> GetAccountRoles(IEnumerable<Role> roles)
        {
            throw new NotImplementedException();
        }

        public RoleService(IRepository<Role> roleRepository, IRepository<AccountRole> accountRoleRepository)
        {
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
        }
    }
}
