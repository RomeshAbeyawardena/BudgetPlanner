using BudgetPlanner.Domains.Constants;
using DNI.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Claims
{
    public interface IPagerClaim
    {
        [Claim(ClaimConstants.PageSizeClaim)]
        int PageSize { get; set; }
        [Claim(ClaimConstants.PageNumberClaim)]
        int PageNumber {  get; set; }
    }
}
