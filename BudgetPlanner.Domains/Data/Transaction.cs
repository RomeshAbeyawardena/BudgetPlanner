using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class Transaction
    {
        public bool Active { get; set; }
        public int BudgetId { get; set; }
        public DateTime Created { get; set; }
    }
}
