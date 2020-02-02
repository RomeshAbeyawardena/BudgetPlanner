﻿using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Services.Abstraction;
using DNI.Shared.Shared.Extensions;
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
        [HttpGet]
        [Route("/Register")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Register(bool isModal = false)
        {
            await Task.CompletedTask;
            var registerAccountViewModel = new RegisterAccountViewModel { IsModal = isModal };
            return View(registerAccountViewModel);
        }

        [Route("/Register")]
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<ActionResult> Register([FromForm]RegisterAccountViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            model.Password = Convert.ToBase64String(
                model.Password.GetBytes(Encoding.UTF8).ToArray());

            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);

            var response = await MediatorService
                .Send<RegisterAccountResponse, RegisterAccountRequest>(new RegisterAccountRequest { Account = mappedAccount });

            if(response.IsSuccessful)
                return RedirectToAction("Login", "Account", new LoginViewModel { EmailAddress = model.EmailAddress });

            ModelState.AddModelError(response.ErrorKey, response.ErrorMessage);

            return View(model);
        }

        [HttpGet]
        [Route("/Login")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Login(string emailAddress)
        {
            await Task.CompletedTask;
            var loginViewModel = new LoginViewModel { EmailAddress = emailAddress };
            return View(loginViewModel);
        }
    }
}