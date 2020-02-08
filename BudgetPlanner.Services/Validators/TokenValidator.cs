using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using DNI.Shared.Contracts.Providers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class TokenValidator : ValidatorBase<CreateTokenRequest>
    {
        public TokenValidator(IAccountService accountService) : base(accountService)
        {
            
        }
    }

    public class ValidateTokenValidator : ValidatorBase<ValidateTokenRequest>
    {

        public ValidateTokenValidator(IAccountService accountService, 
            IEncryptionProvider encryptionProvider, 
            IRequestTokenService requestTokenService) : base(accountService)
        {
            _encryptionProvider = encryptionProvider;
            _requestTokenService = requestTokenService;

            RuleFor(member => member.Token)
                .NotEmpty()
                .MustAsync(BeValid);

        }

        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IRequestTokenService _requestTokenService;

        private async Task<bool> BeValid(string token, CancellationToken cancellationToken)
        {
            var requestToken = new RequestToken {
                Key = token
            };

            var encryptedToken = await _encryptionProvider.Encrypt<RequestToken, Domains.Data.RequestToken>(requestToken);

            encryptedToken = await _requestTokenService.GetRequestToken(encryptedToken.Key);

            return encryptedToken != null;
        }
    }
}
