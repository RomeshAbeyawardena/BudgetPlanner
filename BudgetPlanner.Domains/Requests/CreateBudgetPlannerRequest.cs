using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Requests
{
    public class CreateBudgetPlannerRequest : IRequest<CreateBudgetPlannerResponse>
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
