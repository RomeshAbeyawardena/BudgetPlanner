using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    [RequiresAccount(DataConstants.AccountSessionCookie)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await Task.CompletedTask;
            return View(new HomeViewModel { 
                AccountId = CurrentAccount.Id, 
                LastUpdated = DateTime.Now.AddDays(-30) });
        }

    }
}
