using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetPlannerStatsViewModel : BaseViewModel
    {
        public IEnumerable<BudgetPlannerStat> Statistics { get; set; }
    }
}
