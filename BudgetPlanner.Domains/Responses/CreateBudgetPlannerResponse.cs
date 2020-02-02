using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Domains;

namespace BudgetPlanner.Domains.Responses
{
    public class CreateBudgetPlannerResponse : ResponseBase
    {
        public Budget BudgetPlanner { get; set; }
    }
}
