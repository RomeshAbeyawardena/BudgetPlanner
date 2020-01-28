using BudgetPlanner.Domains.ViewModels;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class HomeController : DefaultControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await Task.CompletedTask;
            return View(new HomeViewModel { LastUpdated = DateTime.Now.AddDays(-30) });
        }

    }
}
