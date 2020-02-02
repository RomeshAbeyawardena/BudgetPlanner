using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RegisterAccountRequest : IRequest<RegisterAccountResponse>
    {
        public Account Account { get; set; }
    }
}
