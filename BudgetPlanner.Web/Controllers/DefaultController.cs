﻿using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class DefaultController : DefaultControllerBase
    {
        [HttpGet]
        [Route("/Default/Error/{statusCode}")]
        public async Task<ActionResult> Error([FromRoute]int statusCode)
        {
            await Task.CompletedTask;

            if(statusCode == 401)
                return RedirectToAction("Login", "Account");

            return View(statusCode);
        }
    }
}