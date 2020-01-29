using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class CreateTransactionRequest : IRequest<CreateTransactionResponse>
    {
        public int Id { get; set; }

        public bool Active { get; set; }
        
        public int BudgetId { get; set; }

        public DateTimeOffset Created { get; set; }
       
        public int TransactionTypeId { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
