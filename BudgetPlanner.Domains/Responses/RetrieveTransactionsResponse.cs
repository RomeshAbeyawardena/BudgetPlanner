using BudgetPlanner.Domains.Data;
using DNI.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RetrieveTransactionsResponse : ResponseBase<IEnumerable<Transaction>>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
