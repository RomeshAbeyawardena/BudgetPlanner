using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
