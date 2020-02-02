using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Behaviours
{
    public class RegisterAccountValidationExceptions : IRequestExceptionHandler<RegisterAccountRequest, RegisterAccountResponse>
    {
        public Task Handle(RegisterAccountRequest request, Exception exception, RequestExceptionHandlerState<RegisterAccountResponse> state, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
