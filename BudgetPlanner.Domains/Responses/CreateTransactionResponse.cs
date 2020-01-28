using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class CreateTransactionResponse
    {
        public bool IsSuccessful { get; set; }
        public object Reference { get; set; }
        public Transaction Transaction { get; set; }
    }
}
