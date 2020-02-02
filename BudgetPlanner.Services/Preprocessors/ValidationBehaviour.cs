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
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : ResponseBase
    {
        private readonly IValidator<TRequest> _validator;

        public async Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result  = await _validator.ValidateAsync(request);
            if(result.IsValid)
                return await next();

            var response = Activator.CreateInstance<TResponse>();

            response.Errors = result.Errors;
            response.IsSuccessful = false;
            return response;
        }

        public ValidationBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
    }
}
