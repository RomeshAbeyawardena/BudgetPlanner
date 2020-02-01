using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.ViewComponents
{
    public class PagerViewComponent : DefaultViewComponentBase
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
