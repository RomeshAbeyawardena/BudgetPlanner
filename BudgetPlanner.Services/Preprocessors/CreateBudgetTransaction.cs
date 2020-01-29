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
    public class CreateBudgetTransaction : IRequestPreProcessor<CreateTransactionRequest>
    {
        private readonly IValidator<CreateTransactionRequest> _validator;

        public async Task Process(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request);
        }

        public CreateBudgetTransaction(IValidator<CreateTransactionRequest> validator)
        {
            _validator = validator;
        }
    }
}
