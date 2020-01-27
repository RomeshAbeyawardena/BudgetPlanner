using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetPanelDashboardViewModel
    {
        public IEnumerable<BudgetPanelDashboardItemViewModel> BudgetPlanners { get; set; }
    }
}
