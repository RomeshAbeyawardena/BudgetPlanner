using BudgetPlanner.Domains.Data;
using DNI.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RetrieveBudgetPlannerResponse : ResponseBase
    {
        public bool IsSuccessful { get; set; }
        public Budget BudgetPlanner { get; set; }
        public decimal OldAmount { get; set; }
        public decimal Amount { get; set; }
    }
}
