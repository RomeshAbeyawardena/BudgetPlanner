using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Dto
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }
        
        public DateTimeOffset? LastUpdated { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
