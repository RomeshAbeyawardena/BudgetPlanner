using BudgetPlanner.Domains.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class BudgetPlannerValidator : AbstractValidator<CreateBudgetPlannerRequest>
    {
        public BudgetPlannerValidator()
        {
            RuleFor(request => request.Reference)
                .NotEmpty().MinimumLength(5);
        }
    }
}
