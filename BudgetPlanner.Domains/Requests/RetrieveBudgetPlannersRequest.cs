using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RetrieveBudgetPlannersRequest : IRequest<RetrieveBudgetPlannersResponse>
    {
        public DateTime LastUpdated { get; set; }
    }
}
