using BudgetPlanner.Domains.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class GetTransactionsViewModel : RetrieveTransactionsResponse
    {
        public decimal OldBalance { get; set; }
    }
}
