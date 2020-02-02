using BudgetPlanner.Domains.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ICookieValidationService
    {
        Task<Account> ValidateCookieToken(string cookieToken);
        
    }
}
