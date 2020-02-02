using BudgetPlanner.Domains.Requests;
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
    public class RegisterAccount : IRequestPreProcessor<RegisterAccountRequest>
    {
        private readonly IValidator<RegisterAccountRequest> _validator;

        public async Task Process(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request);
        }

        public RegisterAccount(IValidator<RegisterAccountRequest> validator)
        {
            _validator = validator;
        }
    }
}
