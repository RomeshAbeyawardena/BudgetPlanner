using BudgetPlanner.Domains.Constants;
using DNI.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Claims
{
    public class RequestTokenClaim
    {
        [Claim(ClaimConstants.RequestTokenClaim)]
        public string Token { get; set; }
    }
}
