using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IAccountAccessService
    {
        Task<IEnumerable<AccessType>> GetAccessTypes(CancellationToken cancellationToken);
        Task<IEnumerable<AccountAccess>> GetAccountAccess(int accountId, 
            int accessTypeId, 
            DateTime fromDate, 
            CancellationToken cancellationToken,
            bool? succeeded = null);
        Task<AccountAccess> SaveAccountAccess(AccountAccess accountAccess, 
            CancellationToken cancellationToken,
            bool saveChanges = true);
        AccessType GetAccessType(IEnumerable<AccessType> accessTypes, string name);
        AccessType GetAccessType(IEnumerable<AccessType> accessTypes, int id);
    }
}
