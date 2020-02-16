using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RetrieveTransactionRequest : IRequest<RetrieveTransactionResponse>
    {
        public int AccountId { get; set; }
        public int TransactionId { get; set; }
        public string Reference { get; set; }
    }
}
