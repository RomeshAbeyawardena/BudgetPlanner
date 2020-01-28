using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Enumerations;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        private IQueryable<Transaction> DefaultTransactionQuery => _transactionRepository.Query(transaction => transaction.Active == true);

        public async Task<IEnumerable<Transaction>> GetTransactions(int budgetId, DateTime fromDate, DateTime toDate)
        {
            var transactionQuery = from transaction in DefaultTransactionQuery
                                   where transaction.BudgetId == budgetId
                                        && (transaction.Created >= fromDate
                                        && transaction.Created <= toDate)
                                    orderby transaction.Created descending
                                    select transaction;

            return await transactionQuery.ToArrayAsync();
        }

        public async Task<decimal> GetBalance(int budgetId)
        {
            var transactionQuery = from transaction in DefaultTransactionQuery
                                   where transaction.BudgetId == budgetId
                                   select transaction;

            var transactionIncomeQuery = from transaction in transactionQuery
                                         where transaction.TransactionTypeId == (int)TransactionType.Income
                                         select transaction;

            var transactionOutgoingQuery = from transaction in transactionQuery
                                           where transaction.TransactionTypeId == (int)TransactionType.Outgoing
                                           select transaction;

            var income = await transactionIncomeQuery.SumAsync(transaction => transaction.Amount);
            var outgoings = await transactionOutgoingQuery.SumAsync(transaction => transaction.Amount);
            
            return income - outgoings;
        }

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
    }
}
