using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class ControllerBase : DefaultControllerBase
    {
        protected Task<Account> CurrentAccount => User.Identity.IsAuthenticated ? AccountManager.GetUserAsync(User) : default; 
        protected UserManager<Account> AccountManager => GetService<UserManager<Account>>();
        protected IBudgetPlannerCacheProvider BudgetPlannerCacheProvider => GetService<IBudgetPlannerCacheProvider>();

        protected void AddModelStateErrors(IEnumerable<IdentityError> identityErrors)
        {
            foreach (var item in identityErrors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }
    }
}
