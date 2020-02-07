using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Claims
{
    public interface IDateRangeClaim
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
