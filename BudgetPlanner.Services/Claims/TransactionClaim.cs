using BudgetPlanner.Contracts.Claims;
using BudgetPlanner.Domains.Constants;
using DNI.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Claims
{
    public class TransactionClaim : DefaultClaim, IPagerClaim, IDateRangeClaim
    {
        [Claim(ClaimConstants.BudgetPlannerIdClaim)]
        public int BudgetPlannerId { get; set; }

        [Claim(ClaimConstants.BudgetPlannerReferenceClaim)]
        public string Reference { get; set; }

        
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
