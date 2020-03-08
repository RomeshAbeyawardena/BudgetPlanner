using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IRequestTokenService
    {
        Task<RequestToken> GetRequestToken(IEnumerable<byte> tokenKey, CancellationToken cancellationToken);
        Task<RequestToken> SaveRequestToken(RequestToken requestToken, CancellationToken cancellationToken);
    }
}
