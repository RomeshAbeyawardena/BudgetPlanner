using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Preprocessors
{
    public class RegisterAccount : IPipelineBehavior<RegisterAccountRequest, RegisterAccountResponse>
    {
        private readonly IValidator<RegisterAccountRequest> _validator;

        public async Task<RegisterAccountResponse> Handle(RegisterAccountRequest request, 
            CancellationToken cancellationToken, RequestHandlerDelegate<RegisterAccountResponse> next)
        {
            var result  = await _validator.ValidateAsync(request);
            if(result.IsValid)
                return await next();

            return new RegisterAccountResponse
            {
                IsSuccessful = false,
                Errors = result.Errors
            };
        }

        public RegisterAccount(IValidator<RegisterAccountRequest> validator)
        {
            _validator = validator;
        }
    }
}
