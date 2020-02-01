using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class TransactionListViewModel
    {
        public int TotalPages { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
