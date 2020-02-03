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

namespace BudgetPlanner.Services.RequestHandlers
{
    public class RegisterAccount : IRequestHandler<RegisterAccountRequest, RegisterAccountResponse>
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly IHashingProvider _hashingProvider;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IAccountService _accountService;

        public async Task<RegisterAccountResponse> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            
            var password = request.Account.Password.GetString(Encoding.UTF8);

            if(!_applicationSettings.EncryptionKeys.TryGetValue(EncryptionKeyConstants.Default, out var defaultSettings))
                throw new KeyNotFoundException();

            var encryptedAccount = await _encryptionProvider.Encrypt<Account, Domains.Data.Account>(request.Account);

            var savedAccount = await _accountService.SaveAccount(encryptedAccount);

            return new RegisterAccountResponse { IsSuccessful = true, SavedAccount = savedAccount };
        }

        public RegisterAccount(ApplicationSettings applicationSettings, IHashingProvider hashingProvider, 
            IEncryptionProvider encryptionProvider, IAccountService accountService)
        {
            _applicationSettings = applicationSettings;
            _hashingProvider = hashingProvider;
            _encryptionProvider = encryptionProvider;
            _accountService = accountService;
        }
    }
}
