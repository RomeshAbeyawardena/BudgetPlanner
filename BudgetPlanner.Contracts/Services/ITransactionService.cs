using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface ITransactionService
    {
        IPagerResult<Transaction> GetPagedTransactions(int budgetId, DateTime fromDate, DateTime toDate);
        IPagerResult<Transaction> GetPagedTransactionsWithLedgers(string reference, DateTime fromDate, DateTime toDate);

        Task<IEnumerable<Transaction>> GetTransactions(int budgetId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetTransactionsWithLedgers(int budgetId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken);
        Task<Transaction> GetTransaction(int transactionId, CancellationToken cancellationToken);
        Task<decimal> GetTotal(int budgetId, Domains.Enumerations.TransactionType transactionType, CancellationToken cancellationToken);
        Task<Transaction> SaveTransaction(Transaction transaction, CancellationToken cancellationToken, bool saveChanges = true);
        Task<Transaction> GetLastTransaction(int budgetId, CancellationToken cancellationToken, bool includeLedger = false);
    }
}
