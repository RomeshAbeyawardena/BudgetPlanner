using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;

namespace BudgetPlanner.Domains.Responses
{
    public class CreateBudgetPlannerResponse
    {
        public bool IsSuccessful { get; set; }
        public Budget BudgetPlanner { get; set; }
    }
}
