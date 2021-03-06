﻿using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Stores
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly IRoleService _roleService;

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            var foundRole = await FindByNameAsync(role.Name, cancellationToken);

            if(foundRole != null)
                return IdentityResult.Failed(IdentityErrors.DuplicateRole);
            
            role.Active = true;
            
            var savedRole = await _roleService.SaveRole(role, cancellationToken);
            return IdentityResult.Success;

        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var foundRole = await FindByNameAsync(role.Name, cancellationToken);

            if(foundRole == null)
                return IdentityResult.Failed(IdentityErrors.RoleNotFound);

            foundRole.Active = false;
            
            var savedRole = await _roleService.SaveRole(role, cancellationToken);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            Dispose(true);   
        }

        protected virtual void  Dispose(bool gc)
        {

        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if(!int.TryParse(roleId, out var id))
                return default;

            return await _budgetPlannerCacheProvider.GetRole(id, cancellationToken);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _budgetPlannerCacheProvider.GetRole(normalizedRoleName, cancellationToken);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return await GetRoleNameAsync(role, cancellationToken);
        }

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Id.ToString());
        }

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Name);
        }

        public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            await SetRoleNameAsync(role, normalizedName, cancellationToken);
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            var foundRole = await FindByNameAsync(role.Name, cancellationToken);
            foundRole.Name = roleName;

            await _roleService.SaveRole(foundRole, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            var foundRole = await FindByNameAsync(role.Name, cancellationToken);

            if(foundRole == null)
                return IdentityResult.Failed(IdentityErrors.RoleNotFound);

            role.Id = foundRole.Id;
            await _roleService.SaveRole(role, cancellationToken);

            return IdentityResult.Success;
        }

        public RoleStore(IBudgetPlannerCacheProvider budgetPlannerCacheProvider, IRoleService roleService)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _roleService = roleService;
        }
    }
}
