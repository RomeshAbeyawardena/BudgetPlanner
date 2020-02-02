using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
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
    public class AccountController : DefaultControllerBase
    {
        public async Task<ActionResult> Register()
        {
            await Task.CompletedTask;
            var registerAccountViewModel = new RegisterAccountViewModel();
            return View(registerAccountViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterAccountViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);

            var response = await MediatorService
                .Send<RegisterAccountResponse, RegisterAccountRequest>(new RegisterAccountRequest { Account = mappedAccount });

            if(response.IsSuccessful)
                return RedirectToAction("Login", "Account", new LoginViewModel { EmailAddress = model.EmailAddress });

            ModelState.AddModelError(response.ErrorKey, response.ErrorMessage);

            return View(model);
        }
    }
}
