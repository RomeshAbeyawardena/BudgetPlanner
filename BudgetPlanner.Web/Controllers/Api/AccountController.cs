using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class AccountController : DefaultApiController
    {
        [HttpPost]
        public async Task<ActionResult> RegisterAccount(RegisterAccountViewModel model)
        {
            var mappedAccount = Map<RegisterAccountViewModel, Account>(model);

            var response = await MediatorService
                .Send(new RegisterAccountRequest { Account = mappedAccount });

            return ResponseResult(response);
        }
    }
}
