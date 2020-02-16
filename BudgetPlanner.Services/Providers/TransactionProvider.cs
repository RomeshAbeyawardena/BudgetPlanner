using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Providers
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionLedgerService _transactionLedgerService;

        public async Task<decimal> GetBalance(int budgetPlannerId, bool useRealtimeData = false)
        {
            if(useRealtimeData)
                return await GetBalanceFromRealtimeData(budgetPlannerId);

            var transaction =  await _transactionService.GetLastTransaction(budgetPlannerId, true);

            if (transaction == null)
                return 0;

            return transaction.TransactionLedgers?.FirstOrDefault()?.NewBalance ?? 0;
        }

        private async Task<decimal> GetBalanceFromRealtimeData(int budgetPlannerId)
        {
            var income = await _transactionService
                .GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Income);
            var outgoing = await _transactionService
                .GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Expense);
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
