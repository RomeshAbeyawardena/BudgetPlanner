using BudgetPlanner.Domains.Constants;
using DNI.Shared.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Claims
{
    public class DefaultClaim
    {
        [Claim(DataConstants.AccountIdClaim)]
        public int AccountId { get; set; }
    }
}
