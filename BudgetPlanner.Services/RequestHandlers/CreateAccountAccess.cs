using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class CreateAccountAccess : IRequestHandler<CreateAccountAccessRequest, CreateAccountAccessResponse>
    {
        private readonly IAccountAccessService _accountAccessService;

        public async Task<CreateAccountAccessResponse> Handle(CreateAccountAccessRequest request, CancellationToken cancellationToken)
        {
           var savedAccountAccess = await _accountAccessService.SaveAccountAccess(request.AccountAccessModel, cancellationToken);

            return Response.Success<CreateAccountAccessResponse>(savedAccountAccess);
        }

        public CreateAccountAccess(IAccountAccessService accountAccessService)
        {
            _accountAccessService = accountAccessService;
        }
    }
}
