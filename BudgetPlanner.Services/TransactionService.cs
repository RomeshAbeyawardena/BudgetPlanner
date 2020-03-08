using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Enumerations;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        private IQueryable<Transaction> DefaultTransactionQuery => _transactionRepository.Query(transaction => transaction.Active == true, false);
        
        private IQueryable<Transaction> BudgetTransactionQuery(int budgetId, IQueryable<Transaction> transactionQuery) => transactionQuery
            .Where(transaction => transaction.BudgetId == budgetId);
        
        private IQueryable<Transaction> BudgetReferenceTransactionQuery(string reference, IQueryable<Transaction> transactionQuery) => _transactionRepository.For(transactionQuery).Include(transaction => transaction.Budget)
            .Where(transaction => transaction.Budget.Reference == reference);

        private IQueryable<Transaction> BudgetTransactionDateRangeQuery(DateTime fromDate, DateTime toDate,
            IQueryable<Transaction> transactionQuery) => transactionQuery
            .Where(transaction => transaction.Created >= fromDate
                                        && transaction.Created <= toDate);

        public async Task<IEnumerable<Transaction>> GetTransactions(int budgetId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery))
                                   orderby transaction.Created descending
                                   select transaction;
            
            return await _transactionRepository.For(transactionQuery).ToArrayAsync(cancellationToken);
        }

        public async Task<decimal> GetTotal(int budgetId, Domains.Enumerations.TransactionType transactionType, CancellationToken cancellationToken)
        {
            var transactionQuery = from transaction in BudgetTransactionQuery(budgetId, DefaultTransactionQuery)
                                   select transaction;

            var transactionTypeId = (int)transactionType;

            var transactionSumQuery = from transaction in transactionQuery
                                      where transaction.TransactionTypeId == transactionTypeId
                                      select transaction;

            return await _transactionRepository.For(transactionSumQuery)
                .ToSumAsync(transaction => transaction.Amount, cancellationToken) ?? 0;
        }

        public async Task<Transaction> SaveTransaction(Transaction transaction, CancellationToken cancellationToken, bool saveChanges = true)
        {
            return await _transactionRepository.SaveChanges(transaction, saveChanges);
        }

        public async Task<Transaction> GetLastTransaction(int budgetId, CancellationToken cancellationToken, bool includeLedger = false)
        {
            var transactionQuery = BudgetTransactionQuery(budgetId, DefaultTransactionQuery); 

            if(includeLedger)
                transactionQuery = _transactionRepository.For(transactionQuery)
                    .Include(transaction => transaction.TransactionLedgers);

            transactionQuery = from transaction in transactionQuery
                                   orderby transaction.Created descending
                                   select transaction;

            return await _transactionRepository
                .For(transactionQuery)
                .ToFirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsWithLedgers(int budgetId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
        {
            var transactionQuery = from transaction in _transactionRepository.For(BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery)))
                                   .Include(transaction => transaction.TransactionLedgers)
                                   orderby transaction.Created descending
                                   select transaction;
            return await _transactionRepository.For(transactionQuery).ToArrayAsync(cancellationToken);
        }

        public IPagerResult<Transaction> GetPagedTransactions(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery))
                        orderby transaction.Created descending
                        select transaction;

            return _transactionRepository.For(transactionQuery)
                .AsPager(transactionQuery);
        }

        public IPagerResult<Transaction> GetPagedTransactionsWithLedgers(string reference, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetReferenceTransactionQuery(reference, DefaultTransactionQuery))
                                   .Include(transaction => transaction.TransactionLedgers)
                                   orderby transaction.Created descending
                                   select transaction;

            return _transactionRepository.GetPager(transactionQuery);
        }

        public async Task<Transaction> GetTransaction(int transactionId, CancellationToken cancellationToken)
        {
            return await _transactionRepository.Find(false, cancellationToken, transactionId);
        }

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
    }
}
