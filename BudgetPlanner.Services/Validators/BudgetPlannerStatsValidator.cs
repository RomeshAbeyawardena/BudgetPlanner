using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class BudgetPlannerStatsValidator : ValidatorBase<BudgetPlannerStatsRequest>
    {
        public BudgetPlannerStatsValidator(IAccountService accountService) : base(accountService)
        {
        }
    }
}
