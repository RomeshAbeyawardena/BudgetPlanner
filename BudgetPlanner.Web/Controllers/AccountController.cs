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
using Microsoft.AspNetCore.Http;
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
        private readonly ApplicationSettings _applicationSettings;
        private readonly ICookieValidationService _cookieValidationService;

        public AccountController(ApplicationSettings applicationSettings, ICookieValidationService cookieValidationService)
        {
            _applicationSettings = applicationSettings;
            _cookieValidationService = cookieValidationService;
        }

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

            AddErrorsToModelState(response);

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

        [HttpPost]
        [Route("/Login")]
        [HeaderValue(HeaderConstants.DismissModalHeaderKey, "true")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View("Login", model);

            var response = await MediatorService
                .Send<LoginResponse,LoginRequest>(new LoginRequest { 
                    EmailAddress = model.EmailAddress, 
                    Password = model.Password 
                });
            
            if(response.IsSuccessful)
            {
                var cookieToken = await _cookieValidationService
                    .CreateCookieToken(config => { 
                        config.Audience = _applicationSettings.Audiences.FirstOrDefault();
                        config.Issuer = _applicationSettings.Audiences.FirstOrDefault();
                        }, response.Account, 
                    _applicationSettings.SessionExpiryInMinutes);
                _cookieValidationService.AppendSessionCookie(Response.Cookies, 
                    DataConstants.AccountSessionCookie, cookieToken, 
                    cookieOptions => { _cookieValidationService
                        .ConfigureCookieOptions(cookieOptions, _applicationSettings.SessionExpiryInMinutes); 
                        cookieOptions.HttpOnly = true;
                        cookieOptions.SameSite = SameSiteMode.Strict;
                        cookieOptions.IsEssential = true;
                        cookieOptions.Domain = Request.Host.Host;
                        });
                return RedirectToAction("Index", "Home");
            }
            AddErrorsToModelState(response);

            return View("Login", model);
        }

    }
}
