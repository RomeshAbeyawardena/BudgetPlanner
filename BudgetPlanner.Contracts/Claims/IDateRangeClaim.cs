using BudgetPlanner.Domains.Constants;
using DNI.Shared.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Claims
{
    public interface IDateRangeClaim
    {
        [Claim(ClaimConstants.FromDateRangeClaim)]
        DateTime FromDate { get; set; }

        [Claim(ClaimConstants.ToDateRangeClaim)]
        DateTime ToDate { get; set; }
    }
}
