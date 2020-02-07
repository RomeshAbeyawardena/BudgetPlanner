using BudgetPlanner.Domains.Constants;
using DNI.Shared.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Claims
{
    public interface IPagerClaim
    {
        [Claim(DataConstants.PageSizeClaim)]
        int PageSize { get; set; }
        [Claim(DataConstants.PageNumberClaim)]
        int PageNumber {  get; set; }
    }
}
