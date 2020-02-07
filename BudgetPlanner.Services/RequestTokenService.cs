using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class RequestTokenService : IRequestTokenService
    {
        private readonly IRepository<RequestToken> _requestTokenRepository;
        private readonly IClockProvider _clockProvider;

        private IQueryable<RequestToken> DefaultQuery => _requestTokenRepository
            .Query(requestToken => requestToken.Created > _clockProvider.DateTimeOffset);

        public async Task<RequestToken> GetRequestToken(IEnumerable<byte> tokenKey)
        {
            var tokenKeyArray = tokenKey.ToArray();
            var query = from requestToken in DefaultQuery 
                        where requestToken.Key == tokenKeyArray
                        select requestToken;

            return await query.SingleOrDefaultAsync();
        }

        public async Task<RequestToken> SaveRequestToken(RequestToken requestToken)
        {
            return await _requestTokenRepository
                .SaveChanges(requestToken);
        }

        public RequestTokenService(IRepository<RequestToken> requestTokenRepository, IClockProvider clockProvider)
        {
            _requestTokenRepository = requestTokenRepository;
            _clockProvider = clockProvider;
        }
    }
}
