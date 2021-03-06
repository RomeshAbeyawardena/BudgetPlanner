﻿using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ITransactionLedgerService
    {
        Task<TransactionLedger> GetTransactionLedger(int transactionId, CancellationToken cancellationToken);
        Task<TransactionLedger> SaveTransactionLedger(TransactionLedger transaction, bool saveChanges, CancellationToken cancellationToken);
    }
}
