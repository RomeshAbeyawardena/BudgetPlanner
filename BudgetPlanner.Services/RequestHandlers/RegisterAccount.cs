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
    public class RegisterAccount : IRequestHandler<RegisterAccountRequest, RegisterAccountResponse>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IAccountService _accountService;

        public async Task<RegisterAccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            var encryptedAccount = await _encryptionProvider.Encrypt<Account, Domains.Data.Account>(request.Account);
            var savedAccount = await _accountService.SaveAccount(encryptedAccount);

            return new RegisterAccountResponse { IsSuccessful = true, SavedAccount = savedAccount };
        }

        public RegisterAccount(IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _encryptionProvider = encryptionProvider;
            _accountService = accountService;
        }
    }
}
