using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        protected ICmsContentProvider CmsContentProvider => GetService<ICmsContentProvider>();

        protected async Task<ViewResult> ViewWithContent<TModel>(string contentPath, string viewName, TModel model,
            IDictionary<string, string> placeholders = null, 
            string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model, 
                placeholders, replaceParameterStart, replaceParameterEnd);
            return View(viewName, model);
        }

        public async Task<string> GetContent(string contentPath, string property, 
            IDictionary<string, string> placeholders = null,
            string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            return await CmsContentProvider.GetContent(contentPath, property, 
                placeholders, replaceParameterStart, replaceParameterEnd);
        }

        protected async Task<ViewResult> ViewWithContent<TModel>(string contentPath, TModel model,
            IDictionary<string, string> placeholders = null, 
            string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model,
                placeholders, replaceParameterStart, replaceParameterEnd);
            return View(model);
        }

        protected void AddModelStateErrors(IEnumerable<IdentityError> identityErrors)
        {
            foreach (var item in identityErrors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }
    }
}
