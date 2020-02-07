using BudgetPlanner.Domains.Constants;
using DNI.Shared.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Claims
{
    public class BudgetPlannerClaim : DefaultClaim
    {
        [Claim(DataConstants.BudgetPlannerIdClaim)]
        public int BudgetPlannerId { get; set; }
        
        [Claim(DataConstants.BudgetPlannerReferenceClaim)]
        public string Reference { get; set; }
    }
}
