using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class AccountPasswordValidator : IPasswordValidator<Account>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Account> manager, Account user, string password)
        {
            
        }
    }
}
