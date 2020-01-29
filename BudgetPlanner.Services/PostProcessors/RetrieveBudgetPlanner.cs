using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.PostProcessors
{
    public class RetrieveBudgetPlanner : IRequestPostProcessor<RetrieveBudgetPlannerRequest, RetrieveBudgetPlannerResponse>
    {
        public Task Process(RetrieveBudgetPlannerRequest request, RetrieveBudgetPlannerResponse response, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
