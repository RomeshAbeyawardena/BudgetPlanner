using BudgetPlanner.Contracts.Providers;
using DNI.Shared.Services.Abstraction;
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

        protected async Task<IViewComponentResult> ViewWithContent<TModel>(string contentPath, TModel model)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model);
            return View(model);
        }

        protected async Task<IViewComponentResult> ViewWithContent<TModel>(string contentPath, string viewName, TModel model)
        {
            model = await CmsContentProvider.PopulateContent(contentPath, model);
            return View(viewName, model);
        }

    }
}
