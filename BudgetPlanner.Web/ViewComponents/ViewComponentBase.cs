using BudgetPlanner.Contracts.Providers;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.ViewComponents
{
    public abstract class ViewComponentBase : DefaultViewComponentBase
    { 
        protected ICmsContentProvider CmsContentProvider => GetService<ICmsContentProvider>();

        protected async Task<IViewComponentResult> ViewWithContent<TModel>(string contentPath, TModel model,
            IDictionary<string, string> placeholders = null, 
            string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model, placeholders, replaceParameterStart, replaceParameterEnd);
            return View(model);
        }

        protected async Task<IViewComponentResult> ViewWithContent<TModel>(string contentPath, string viewName, TModel model,
            IDictionary<string, string> placeholders = null, 
            string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model, placeholders, replaceParameterStart, replaceParameterEnd);
            return View(viewName, model);
        }

    }
}
