using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using FluentValidation;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Preprocessors
{
    public class CreateBudgetPlanner : IRequestPreProcessor<CreateBudgetPlannerRequest>
    {
        private readonly IValidator<CreateBudgetPlannerRequest> _validator;

        public async Task Process(CreateBudgetPlannerRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request);
        }

        public CreateBudgetPlanner(IValidator<CreateBudgetPlannerRequest> validator)
        {
            _validator = validator;
        }
    }
}
