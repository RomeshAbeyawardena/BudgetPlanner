﻿using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ITransactionService
    {
        IPagerResult<Transaction> GetPagedTransactions(int budgetId, DateTime fromDate, DateTime toDate);
        IPagerResult<Transaction> GetPagedTransactionsWithLedgers(string reference, DateTime fromDate, DateTime toDate);

        Task<IEnumerable<Transaction>> GetTransactions(int budgetId, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Transaction>> GetTransactionsWithLedgers(int budgetId, DateTime fromDate, DateTime toDate);

        Task<decimal> GetTotal(int budgetId, Domains.Enumerations.TransactionType transactionType);
        Task<Transaction> SaveTransaction(Transaction transaction);
        Task<Transaction> GetLastTransaction(int budgetId, bool includeLedger = false);
    }
}
