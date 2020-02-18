using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetPlannerDetailsViewModel : BaseViewModel
    {
        public BudgetPlannerStatsViewModel BudgetPlannerStats { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public decimal Balance { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
