using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RetrieveTransactionTagsRequest : IRequest<RetrieveTransactionTagsResponse>
    {
        public int TransactionId { get; set; }
    }
}
