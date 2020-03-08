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
        
        private IQueryable<Transaction> BudgetReferenceTransactionQuery(string reference, IQueryable<Transaction> transactionQuery) => transactionQuery.Include(transaction => transaction.Budget)
            .Where(transaction => transaction.Budget.Reference == reference);

        private IQueryable<Transaction> BudgetTransactionDateRangeQuery(DateTime fromDate, DateTime toDate,
            IQueryable<Transaction> transactionQuery) => transactionQuery
            .Where(transaction => transaction.Created >= fromDate
                                        && transaction.Created <= toDate);

        public async Task<IEnumerable<Transaction>> GetTransactions(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery))
                                   orderby transaction.Created descending
                                   select transaction;
            
            return await transactionQuery.ToArrayAsync();
        }

        public async Task<decimal> GetTotal(int budgetId, Domains.Enumerations.TransactionType transactionType)
        {
            var transactionQuery = from transaction in BudgetTransactionQuery(budgetId, DefaultTransactionQuery)
                                   select transaction;

            var transactionTypeId = (int)transactionType;

            var transactionSumQuery = from transaction in transactionQuery
                                      where transaction.TransactionTypeId == transactionTypeId
                                      select transaction;

            return await transactionSumQuery.SumAsync(transaction => transaction.Amount);
        }

        public async Task<Transaction> SaveTransaction(Transaction transaction, bool saveChanges = true)
        {
            return await _transactionRepository.SaveChanges(transaction, saveChanges);
        }

        public async Task<Transaction> GetLastTransaction(int budgetId, bool includeLedger = false)
        {
            var transactionQuery = BudgetTransactionQuery(budgetId, DefaultTransactionQuery); 

            if(includeLedger)
                transactionQuery = transactionQuery
                    .Include(transaction => transaction.TransactionLedgers);

            transactionQuery = from transaction in transactionQuery
                                   orderby transaction.Created descending
                                   select transaction;

            return await transactionQuery.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsWithLedgers(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery))
                                   .Include(transaction => transaction.TransactionLedgers)
                                   orderby transaction.Created descending
                                   select transaction;
            return await transactionQuery.ToArrayAsync();
        }

        public IPagerResult<Transaction> GetPagedTransactions(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in BudgetTransactionDateRangeQuery(fromDate, toDate, 
                BudgetTransactionQuery(budgetId, DefaultTransactionQuery))
                        orderby transaction.Created descending
                        select transaction;

            return _transactionRepository.GetPager(transactionQuery);
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
