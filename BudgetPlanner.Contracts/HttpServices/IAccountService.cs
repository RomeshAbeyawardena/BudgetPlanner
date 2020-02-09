using BudgetPlanner.Domains.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.HttpServices
{
    public interface IAccountService : IHttpService
    {
        Task<LoginResponse> Login(string emailAddress, string password);
    }
}
