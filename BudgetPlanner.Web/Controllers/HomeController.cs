using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public async Task<ActionResult> Index()
        {
            await Task.CompletedTask;
            return View(new HomeViewModel { 
                LastUpdated = DateTime.Now.AddDays(-30) });
        }

    }
}
