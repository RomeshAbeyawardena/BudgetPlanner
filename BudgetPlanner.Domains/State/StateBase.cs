using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.State
{
    public class StateBase
    {
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
    }
}
