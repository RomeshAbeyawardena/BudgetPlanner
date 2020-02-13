using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<AccessType>> GetAccessTypes()
        {
            return await DefaultAccessTypeQuery.ToArrayAsync();
                   
        }

        public async Task<IEnumerable<AccountAccess>> GetAccountAccess(int accountId, int accessTypeId, DateTime fromDate, bool? succeeded = null)
        {
            var accountAccessQuery = from accountAccess in DefaultAccountAccessQuery
                                     where accountAccess.Created > fromDate
                                     select accountAccess;

            if(succeeded.HasValue)
                accountAccessQuery = from accountAccess in accountAccessQuery
                                     where accountAccess.Succeeded == succeeded.Value
                                     select accountAccess;

            return await accountAccessQuery.ToArrayAsync();
        }

        public async Task<AccountAccess> SaveAccountAccess(AccountAccess accountAccess, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return await _accountAccessRepository.SaveChanges(accountAccess, saveChanges, cancellationToken);
        }

        public AccountAccessService(IRepository<AccessType> accessTypeRepository, 
            IRepository<AccountAccess> accountAccessRepository)
        {
            _accessTypeRepository = accessTypeRepository;
            _accountAccessRepository = accountAccessRepository;
        }
    }
}
