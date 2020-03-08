using BudgetPlanner.Domains.Constants;
using DNI.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Claims
{
    public class BudgetPlannerClaim : DefaultClaim
    {
        [Claim(ClaimConstants.BudgetPlannerIdClaim)]
        public int BudgetPlannerId { get; set; }
        
        [Claim(ClaimConstants.BudgetPlannerReferenceClaim)]
        public string Reference { get; set; }
    }
}
