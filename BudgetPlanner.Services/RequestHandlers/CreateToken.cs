using BudgetPlanner.Contracts.Services;
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
    public class CreateToken : IRequestHandler<CreateTokenRequest, CreateTokenResponse>
    {
        private readonly IRequestTokenService _requestTokenService;

        public Task<CreateTokenResponse> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public CreateToken(IRandomStringGenerator randomStringGenerator, IRequestTokenService requestTokenService)
        {
            _requestTokenService = requestTokenService;
        }
    }
}
