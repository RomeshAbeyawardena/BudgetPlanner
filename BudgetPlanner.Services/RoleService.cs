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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<AccountRole> _accountRoleRepository;

        public IQueryable<AccountRole> DefaultAccountRoleQuery => _accountRoleRepository.Query(accountRole => accountRole.Active, false);
        
        public IQueryable<Role> DefaultRoleQuery => _roleRepository.Query(role => role.Active, false);
        public async Task<IEnumerable<AccountRole>> GetAccountRoles(int accountId, CancellationToken cancellationToken)
        {
            var accountRoleQuery = from accountRole in _accountRoleRepository.For(DefaultAccountRoleQuery).Include(accountRole => accountRole.Role)
            where accountRole.AccountId == accountId
            select accountRole;

            return await _accountRoleRepository.For(accountRoleQuery).ToArrayAsync(cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken)
        {
            var roleQuery = from role in DefaultRoleQuery
                            select role;

            return await _roleRepository.For(roleQuery).ToArrayAsync(cancellationToken);
        }

        public async Task<AccountRole> SaveAccountRole(AccountRole accountRole, CancellationToken cancellationToken)
        {
            return await _accountRoleRepository.SaveChanges(accountRole);
        }

        public async Task<Role> SaveRole(Role role, CancellationToken cancellationToken)
        {
            return await _roleRepository.SaveChanges(role);
        }

        public IEnumerable<Role> GetRoles(IEnumerable<AccountRole> accountRoles)
        {
            return accountRoles.Select(accountRole => accountRole.Role);
        }

        public IEnumerable<Role> GetRoles(IEnumerable<Role> roles, IEnumerable<string> roleNames)
        {
            return roles.Where(role => roleNames.Contains(role.Name));
        }

        public async Task<IEnumerable<AccountRole>> GetAccountRoles(IEnumerable<Role> roles, CancellationToken cancellationToken)
        {
            var roleIds = roles.Select(role => role.Id);
            var accountRolesQuery = from accountRole in DefaultAccountRoleQuery
            where roleIds.Contains(accountRole.RoleId)
            select accountRole;

            return await _accountRoleRepository.For(accountRolesQuery).ToArrayAsync(cancellationToken);
        }

        public async Task<Role> GetRole(int id, CancellationToken cancellationToken)
        {
            return await _roleRepository.Find(false, cancellationToken, id);
        }

        public async Task<Role> GetRole(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var roleQuery = from role in DefaultRoleQuery
                            where role.Name == normalizedRoleName
                            select role;

            return  await _roleRepository.For(roleQuery).ToFirstOrDefaultAsync(cancellationToken);
        }

        public RoleService(IRepository<Role> roleRepository, IRepository<AccountRole> accountRoleRepository)
        {
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
        }
    }
}
