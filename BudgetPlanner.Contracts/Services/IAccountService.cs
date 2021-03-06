﻿using BudgetPlanner.Contracts.Enumeration;
using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IAccountService
    {
        Task<Account> SaveAccount(Account account, CancellationToken cancellationToken = default);
        Task<Account> GetAccount(IEnumerable<byte> encryptedEmailAddress, CancellationToken cancellationToken);
        Task<Account> GetAccount(int accountId, EntityUsage findUsage, CancellationToken cancellationToken);
    }
}
