using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts
{
    public interface IStateBase
    {
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
    }
}
