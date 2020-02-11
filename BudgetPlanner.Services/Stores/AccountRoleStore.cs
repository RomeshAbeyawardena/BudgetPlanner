using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Stores
{
    public partial class AccountStore : IUserRoleStore<Account>
    {
        public async Task AddToRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRoles();
            var accountRoles = await _roleService.GetAccountRoles(user.Id);
            var foundRole = roles.FirstOrDefault(role => role.Name == roleName);

            if(foundRole == null || accountRoles.Any(accountRole => accountRole.RoleId == foundRole.Id))
                return;

            await _roleService.SaveAccountRole(new Domains.Data.AccountRole { AccountId = user.Id, RoleId = foundRole.Id });
        }

        public async Task<IList<string>> GetRolesAsync(Account user, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAccountRoles(user.Id);
                
            return roles.Select(role => role.Role.Name).ToList();
        }

        public async Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRoles();
            var accountRoles = await _roleService.GetAccountRoles(roles.Where(role => role.Name == roleName));

            var accounts = accountRoles.Select(accountRole => accountRole.Account);

            var decryptedAccounts = await _encryptionHelper.Decrypt<Domains.Data.Account, Account>(accounts);

            return decryptedAccounts.ToList();
        }

        public async Task<bool> IsInRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            var roles = await GetRolesAsync(user, cancellationToken);
            return roles.Contains(roleName);
        }

        public Task RemoveFromRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
