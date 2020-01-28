using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class Budget
    {
        public string Reference { get; set; }
        public bool Active { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
