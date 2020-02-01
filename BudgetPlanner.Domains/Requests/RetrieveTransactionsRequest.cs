using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RetrieveTransactionsRequest : IRequest<RetrieveTransactionsResponse>
    {
        public int BudgetId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
