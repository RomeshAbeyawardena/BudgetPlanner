using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using BudgetPlanner.Web.Attributes;
using DNI.Core.Domains;
using DNI.Core.Services.Abstraction;
using DNI.Core.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Account> _signInManager;

        private async Task AuditAccountAccess(Account account, Microsoft.AspNetCore.Identity.SignInResult identityResult, CancellationToken cancellationToken)
        {
            if (account == null)
                return;

            var loginAccessType = await BudgetPlannerCacheProvider.GetAccessType(DataConstants.LoginAccess, cancellationToken);

            await MediatorService.Send(new CreateAccountAccessRequest
            {
                AccountAccessModel = new Domains.Data.AccountAccess
                {
                    Succeeded = identityResult.Succeeded,
                    AccountId = account.Id,
                    Active = true,
                    AccessTypeId = loginAccessType.Id
                }
            });

        }

        public AccountController(SignInManager<Account> signInManager)
        {
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/Register")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Register(bool isModal = false)
        {
            var registerAccountViewModel = new RegisterAccountViewModel { IsModal = isModal };
            return await ViewWithContent(ContentConstants.RegisterContentPath, registerAccountViewModel);
        }

        [Route("/Register")]
        [ValidateAntiForgeryToken, HttpPost, AllowAnonymous]
        public async Task<ActionResult> Register([FromForm]RegisterAccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Password = Convert.ToBase64String(model.Password
                .GetBytes(Encoding.UTF8)
                .ToArray());

            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);
            var result = await AccountManager.CreateAsync(mappedAccount);

            if (result.Succeeded)
                return RedirectToAction("Login", "Account", new LoginViewModel { EmailAddress = model.EmailAddress });

            AddModelStateErrors(result.Errors);
            //AddErrorsToModelState(response);

            return await ViewWithContent(ContentConstants.RegisterContentPath, model);
        }

        [HttpGet, AllowAnonymous, Route("/login")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Login(string emailAddress)
        {
            var loginViewModel = new LoginViewModel { EmailAddress = emailAddress };
            
            return await ViewWithContent(ContentConstants.LoginContentPath, loginViewModel);
        }

        [HttpPost, AllowAnonymous, Route("/login")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new ArgumentException();

                var account = await AccountManager.FindByNameAsync(model.EmailAddress);

                if (account == null)
                    throw new ArgumentException("Invalid e-mail address or password", string.Empty);

                var failedAttempts = await AccountManager.GetAccessFailedCountAsync(account);

                if (failedAttempts > 5)
                    throw new ArgumentException("Your account has been locked out for having too many failed attempts. Please try again later", string.Empty);

                var result = await _signInManager
                    .PasswordSignInAsync(new Account { EmailAddress = model.EmailAddress },
                        model.Password,
                        model.RememberMe,
                        false);

                await AuditAccountAccess(account, result, cancellationToken);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    throw new ArgumentException("Invalid e-mail address or password", string.Empty);
            }
            catch (ArgumentException ex)
            {
                if(ex.ParamName != null)
                    ModelState.AddModelError(ex.ParamName, ex.Message);

                return await ViewWithContent(ContentConstants.LoginContentPath, model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

    }
}
