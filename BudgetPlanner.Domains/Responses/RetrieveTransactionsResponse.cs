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
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
