using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class BudgetPlannerStat
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal ClosingBalance { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalIncome { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalExpenses { get; set; }
    }
}
