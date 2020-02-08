using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts.Generators;
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
    public class ValidateToken : IRequestHandler<ValidateTokenRequest, ValidateTokenResponse>
    {
        private readonly IClockProvider _clockProvider;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IRequestTokenService _requestTokenService;


        public async Task<ValidateTokenResponse> Handle(ValidateTokenRequest request, CancellationToken cancellationToken)
        {
            var requestToken = new RequestToken {
                Key = request.Token
            };

            var encryptedToken = await _encryptionProvider.Encrypt<RequestToken, Domains.Data.RequestToken>(requestToken);

            encryptedToken = await _requestTokenService.GetRequestToken(encryptedToken.Key);

            encryptedToken.Expires = _clockProvider.DateTimeOffset;

            await _requestTokenService.SaveRequestToken(encryptedToken);

            return new ValidateTokenResponse { IsSuccessful = true };
        }

        public ValidateToken(IClockProvider clockProvider, 
            IEncryptionProvider encryptionProvider,
            IRequestTokenService requestTokenService)
        {
            _clockProvider = clockProvider;
            _encryptionProvider = encryptionProvider;
            _requestTokenService = requestTokenService;
        }
    }
}
