using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Generators;
using DNI.Core.Contracts.Providers;
using DNI.Core.Domains;
using DNI.Core.Shared.Extensions;
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
        private readonly IClockProvider _clockProvider;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IRequestTokenService _requestTokenService;

        public async Task<CreateTokenResponse> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
        {
            var generatedString = _randomStringGenerator
                .GenerateString(CharacterType.Lowercase | CharacterType.Uppercase | CharacterType.Numerics, 64);

            var requestToken = new Domains.Dto.RequestToken {
                Key = generatedString,
                Expires = _clockProvider.DateTimeOffset
                    .AddMinutes(request.ValidityPeriodInMinutes)
            };

            var savedRequestToken = await _requestTokenService.SaveRequestToken(await _encryptionProvider
                .Encrypt<Domains.Dto.RequestToken, RequestToken>(requestToken));

            return Response.Success<CreateTokenResponse>(await _encryptionProvider
                .Decrypt<RequestToken, Domains.Dto.RequestToken>(savedRequestToken));
        }

        public CreateToken(IClockProvider clockProvider, 
            IEncryptionProvider encryptionProvider,
            IRandomStringGenerator randomStringGenerator, 
            IRequestTokenService requestTokenService)
        {
            _clockProvider = clockProvider;
            _encryptionProvider = encryptionProvider;
            _randomStringGenerator = randomStringGenerator;
            _requestTokenService = requestTokenService;
        }
    }
}
