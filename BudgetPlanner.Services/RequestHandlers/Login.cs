using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class Login : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IEncryptionProvider _encryptionProvider;

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var encryptedAccount =_encryptionProvider.Encrypt<Account, Domains.Data.Account>(new Account{ EmailAddress = request.EmailAddress, Password = request.Password });

            await _accountService.GetAccount();
        }

        public Login(IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _accountService = accountService;
            _encryptionProvider = encryptionProvider;
        }
    }
}
