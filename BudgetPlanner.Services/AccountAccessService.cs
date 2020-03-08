using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class AccountAccessService : IAccountAccessService
    {
        private readonly IRepository<AccessType> _accessTypeRepository;
        private readonly IRepository<AccountAccess> _accountAccessRepository;

        private IQueryable<AccountAccess> DefaultAccountAccessQuery => _accountAccessRepository.Query(accountAccess => accountAccess.Active, false);
        private IQueryable<AccessType> DefaultAccessTypeQuery => _accessTypeRepository.Query(accountAccess => accountAccess.Active, false);

        public AccessType GetAccessType(IEnumerable<AccessType> accessTypes, string name)
        {
            return accessTypes.FirstOrDefault(accessType => accessType.Name == name);
        }

        public async Task<IEnumerable<AccessType>> GetAccessTypes(CancellationToken cancellationToken)
        {
            return await _accessTypeRepository
                .For(DefaultAccessTypeQuery)
                .ToArrayAsync(cancellationToken);
                   
        }

        public async Task<IEnumerable<AccountAccess>> GetAccountAccess(int accountId, int accessTypeId, 
            DateTime fromDate, CancellationToken cancellationToken, bool? succeeded = null)
        {
            var accountAccessQuery = from accountAccess in DefaultAccountAccessQuery
                                     where accountAccess.Created > fromDate
                                     select accountAccess;

            if(succeeded.HasValue)
                accountAccessQuery = from accountAccess in accountAccessQuery
                                     where accountAccess.Succeeded == succeeded.Value
                                     select accountAccess;

            return await _accountAccessRepository
                .For(accountAccessQuery)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<AccountAccess> SaveAccountAccess(AccountAccess accountAccess, CancellationToken cancellationToken, bool saveChanges = true)
        {
            return await _accountAccessRepository.SaveChanges(accountAccess, saveChanges, cancellationToken: cancellationToken);
        }

        public AccessType GetAccessType(IEnumerable<AccessType> accessTypes, int id)
        {
            return accessTypes.FirstOrDefault(accessType => accessType.Id == id);
        }

        public AccountAccessService(IRepository<AccessType> accessTypeRepository, 
            IRepository<AccountAccess> accountAccessRepository)
        {
            _accessTypeRepository = accessTypeRepository;
            _accountAccessRepository = accountAccessRepository;
        }
    }
}
