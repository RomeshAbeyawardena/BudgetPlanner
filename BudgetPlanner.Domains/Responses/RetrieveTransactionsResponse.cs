using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RetrieveTransactionsResponse
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
