using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetStatisticRequestViewModel
    {
        public string Reference { get; set; }
        public int BudgetId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int AccountId { get; set; }
    }
}
