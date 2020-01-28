using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class CreateBudgetPlanner : IRequestHandler<CreateBudgetPlannerRequest, CreateBudgetPlannerResponse>
    {
        public async Task<CreateBudgetPlannerResponse> Handle(CreateBudgetPlannerRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
