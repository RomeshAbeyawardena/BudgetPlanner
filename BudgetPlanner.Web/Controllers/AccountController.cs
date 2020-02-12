using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Shared.Domains;
using DNI.Shared.Services.Abstraction;
using DNI.Shared.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly ICookieValidationService _cookieValidationService;

        public AccountController(ApplicationSettings applicationSettings, 
            UserManager<Account> userManager, 
            SignInManager<Account> signInManager, ICookieValidationService cookieValidationService)
        {
            _applicationSettings = applicationSettings;
            _userManager = userManager;
            _signInManager = signInManager;
            _cookieValidationService = cookieValidationService;
        }
        [AllowAnonymous]
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
        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> Register([FromForm]RegisterAccountViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            
            model.Password = Convert.ToBase64String(model.Password
                .GetBytes(Encoding.UTF8)
                .ToArray());

            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);
            var result = await _userManager.CreateAsync(mappedAccount);
            
            if(result.Succeeded)
                return RedirectToAction("Login", "Account", new LoginViewModel { EmailAddress = model.EmailAddress });

            AddModelStateErrors(result.Errors);
            //AddErrorsToModelState(response);

            return View(model);
        }

        [HttpGet]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true"), AllowAnonymous]
        public async Task<ActionResult> Login(string emailAddress)
        {
            await Task.CompletedTask;
            var loginViewModel = new LoginViewModel { EmailAddress = emailAddress };
            return View(loginViewModel);
        }

        [HttpPost]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true"), AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View("Login", model);
            
            var s = await _signInManager.PasswordSignInAsync(new Account { EmailAddress = model.EmailAddress }, model.Password, false, false);
            
            if(s.Succeeded)
                return RedirectToAction("Index", "Home");
            
            return View("Login", model);
        }

        [HttpGet]
        
        public async Task<ActionResult> Logout()
        {
            await Task.CompletedTask;
            Response.Cookies.Delete(DataConstants.AccountSessionCookie);
            return View();
        }

    }
}
