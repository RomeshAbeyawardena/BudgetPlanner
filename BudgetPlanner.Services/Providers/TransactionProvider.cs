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

        public async Task<decimal> GetBalance(int budgetPlannerId)
        {
            var income = await _transactionService.GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Income);
            var outgoing = await _transactionService.GetTotal(budgetPlannerId, Domains.Enumerations.TransactionType.Expense);
            return income - outgoing;
        }

        public TransactionProvider(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}
