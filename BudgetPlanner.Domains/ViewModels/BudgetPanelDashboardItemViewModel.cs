using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetPanelDashboardItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Reference { get; set; }
    }
}
