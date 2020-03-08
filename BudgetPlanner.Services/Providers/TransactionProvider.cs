using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Providers
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionLedgerService _transactionLedgerService;

        public async Task<decimal> GetBalance(int budgetPlannerId, CancellationToken cancellationToken, bool useRealtimeData = false)
        {
            if(useRealtimeData)
                return await GetBalanceFromRealtimeData(budgetPlannerId, cancellationToken);

            var transaction =  await _transactionService
                .GetLastTransaction(budgetPlannerId, cancellationToken, true);

            if (transaction == null)
                return 0;

            return transaction.TransactionLedgers?.FirstOrDefault()?.NewBalance ?? 0;
        }

        private async Task<decimal> GetBalanceFromRealtimeData(int budgetPlannerId, CancellationToken cancellationToken)
        {
            var income = await _transactionService
                .GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Income, cancellationToken);
            var outgoing = await _transactionService
                .GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Expense, cancellationToken);
            return income - outgoing;
        }

        public TransactionProvider(ITransactionService transactionService, 
            ITransactionLedgerService transactionLedgerService)
        {
            _transactionService = transactionService;
            _transactionLedgerService = transactionLedgerService;
        }
    }
}
