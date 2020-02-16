using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts.Providers;
using DNI.Shared.Shared.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BudgetPlanner.Domains.Constants;
using DNI.Shared.Domains;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RegisterAccount : IRequestHandler<RegisterAccountRequest, RegisterAccountResponse>
    {
        private readonly IHashingProvider _hashingProvider;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IAccountService _accountService;

        public async Task<RegisterAccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            var encryptedAccount = await _encryptionProvider.Encrypt<Account, Domains.Data.Account>(request.Account);

            var savedAccount = await _accountService.SaveAccount(encryptedAccount);

            var account = await _encryptionProvider.Decrypt<Domains.Data.Account, Account>(savedAccount);

            return Response.Success<RegisterAccountResponse>(account);
        }

        public RegisterAccount(IHashingProvider hashingProvider, 
            IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _hashingProvider = hashingProvider;
            _encryptionProvider = encryptionProvider;
            _accountService = accountService;
        }
    }
}
