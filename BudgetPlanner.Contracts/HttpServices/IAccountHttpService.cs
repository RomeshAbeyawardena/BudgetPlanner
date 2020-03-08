using BudgetPlanner.Domains.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.HttpServices
{
    public interface IAccountHttpService : IHttpService
    {
        Task<LoginResponse> Login(string emailAddress, string password, CancellationToken cancellationToken);
    }
}
