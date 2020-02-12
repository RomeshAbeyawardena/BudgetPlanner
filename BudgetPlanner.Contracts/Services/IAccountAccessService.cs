﻿using BudgetPlanner.Domains.Data;
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
        Task<IEnumerable<AccountAccess>> GetAccountAccess(int accountId, 
            int accessTypeId, 
            DateTime fromDate, 
            bool? succeeded = null);
        Task<AccountAccess> SaveAccountAccess(AccountAccess accountAccess, 
            bool saveChanges = true, 
            CancellationToken cancellationToken = default);
    }
}
