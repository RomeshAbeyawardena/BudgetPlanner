using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class RetrieveBudgetPlannerRequest : IRequest<RetrieveBudgetPlannerResponse>
    {
        public int? BudgetPlannerId { get; set; }
        public string Reference { get; set; }
    }
}
