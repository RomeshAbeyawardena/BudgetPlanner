using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Providers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class RequestTokenService : IRequestTokenService
    {
        private readonly IRepository<RequestToken> _requestTokenRepository;
        private readonly IClockProvider _clockProvider;

        private IQueryable<RequestToken> DefaultQuery => _requestTokenRepository
            .Query(requestToken => requestToken.Expires > _clockProvider.DateTimeOffset, false);

        public async Task<RequestToken> GetRequestToken(IEnumerable<byte> tokenKey, CancellationToken cancellationToken)
        {
            var tokenKeyArray = tokenKey.ToArray();
            var query = from requestToken in DefaultQuery 
                        where requestToken.Key == tokenKeyArray
                        select requestToken;

            return await _requestTokenRepository.For(query).ToSingleOrDefaultAsync(cancellationToken);
        }

        public async Task<RequestToken> SaveRequestToken(RequestToken requestToken, CancellationToken cancellationToken)
        {
            return await _requestTokenRepository
                .SaveChanges(requestToken, cancellationToken : cancellationToken);
        }

        public RequestTokenService(IRepository<RequestToken> requestTokenRepository, IClockProvider clockProvider)
        {
            _requestTokenRepository = requestTokenRepository;
            _clockProvider = clockProvider;
        }
    }
}
